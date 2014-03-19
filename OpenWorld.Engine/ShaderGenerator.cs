using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace OpenWorld.Engine
{
	partial class Shader
	{
		/// <summary>
		/// Compiles a shader and returns a new shader class.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static Shader CompileNew(string source)
		{
			bool hasTesselation;
			int programID = GL.CreateProgram();

			CompileAndLinkShader(programID, source, out hasTesselation);

			TypeBuilder tb = GetTypeBuilder("RuntimeShader");
			ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

			int num;
			GL.GetProgramInterface(programID, ProgramInterface.Uniform, ProgramInterfaceParameter.ActiveResources, out num);
			for (int i = 0; i < num; i++)
			{
				int len;
				StringBuilder builder = new StringBuilder(256);
				GL.GetProgramResourceName(programID, ProgramInterface.Uniform, i, 256, out len, builder);
				string name = builder.ToString();

				int size;
				ActiveUniformType type;
				GL.GetActiveUniform(programID, i, out size, out type);

				var attribs = new CustomAttributeBuilder(
					typeof(UniformAttribute).GetConstructor(
						new[] { typeof(string) }),
						new object[] { name });
				Type propertyType = null;
				switch (type)
				{
					case ActiveUniformType.Float:
						propertyType = typeof(float);
						break;
					case ActiveUniformType.FloatVec2:
						propertyType = typeof(Vector2);
						break;
					case ActiveUniformType.FloatVec3:
						propertyType = typeof(Vector3);
						break;
					case ActiveUniformType.FloatVec4:
						propertyType = typeof(Vector4);
						break;
					case ActiveUniformType.FloatMat4:
						propertyType = typeof(Matrix4);
						break;
					default:
						continue;
				}
				string propertyName = name.Substring(0, 1).ToUpper() + (name.Length > 0 ? name.Substring(1) : "");
				CreateProperty(tb, propertyName, propertyType, attribs);
			}

			Type objectType = tb.CreateType();
			var shader = Activator.CreateInstance(objectType) as Shader;
			shader.programID = programID;
			return shader;
		}

		private static TypeBuilder GetTypeBuilder(string typeName)
		{
			var an = new AssemblyName("OpenWorld.Engine.Dynamics");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
			TypeBuilder tb = moduleBuilder.DefineType(typeName
								, TypeAttributes.Public |
								TypeAttributes.Class |
								TypeAttributes.AutoClass |
								TypeAttributes.AnsiClass |
								TypeAttributes.BeforeFieldInit |
								TypeAttributes.AutoLayout,
								typeof(Shader));
			return tb;
		}

		private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, CustomAttributeBuilder attribs)
		{
			FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

			PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
			if (attribs != null)
				propertyBuilder.SetCustomAttribute(attribs);

			MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
			ILGenerator getIl = getPropMthdBldr.GetILGenerator();

			getIl.Emit(OpCodes.Ldarg_0);
			getIl.Emit(OpCodes.Ldfld, fieldBuilder);
			getIl.Emit(OpCodes.Ret);

			MethodBuilder setPropMthdBldr =
				tb.DefineMethod("set_" + propertyName,
				  MethodAttributes.Public |
				  MethodAttributes.SpecialName |
				  MethodAttributes.HideBySig,
				  null, new[] { propertyType });

			ILGenerator setIl = setPropMthdBldr.GetILGenerator();
			Label modifyProperty = setIl.DefineLabel();
			Label exitSet = setIl.DefineLabel();

			setIl.MarkLabel(modifyProperty);
			setIl.Emit(OpCodes.Ldarg_0);
			setIl.Emit(OpCodes.Ldarg_1);
			setIl.Emit(OpCodes.Stfld, fieldBuilder);

			setIl.Emit(OpCodes.Nop);
			setIl.MarkLabel(exitSet);
			setIl.Emit(OpCodes.Ret);

			propertyBuilder.SetGetMethod(getPropMthdBldr);
			propertyBuilder.SetSetMethod(setPropMthdBldr);
		}
	}
}
