using System;
using System.Runtime.InteropServices;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Wrapper of oggvorbis lib.
    /// </summary>
    public partial class NativeConstants
    {

        /// _OGG_H -> 
        /// Error generating expression: Der Wert darf nicht NULL sein.
        ///Parametername: node
        public const string _OGG_H = "";

        /// _OS_TYPES_H -> 
        /// Error generating expression: Der Wert darf nicht NULL sein.
        ///Parametername: node
        public const string _OS_TYPES_H = "";

        /// _ogg_malloc -> malloc
        /// Error generating expression: Value malloc is not resolved
        public const string _ogg_malloc = "malloc";

        /// _ogg_calloc -> calloc
        /// Error generating expression: Value calloc is not resolved
        public const string _ogg_calloc = "calloc";

        /// _ogg_realloc -> realloc
        /// Error generating expression: Value realloc is not resolved
        public const string _ogg_realloc = "realloc";

        /// _ogg_free -> free
        /// Error generating expression: Value free is not resolved
        public const string _ogg_free = "free";

        /// _vorbis_codec_h_ -> 
        /// Error generating expression: Der Wert darf nicht NULL sein.
        ///Parametername: node
        public const string _vorbis_codec_h_ = "";

        /// OV_FALSE -> -1
        public const int OV_FALSE = -1;

        /// OV_EOF -> -2
        public const int OV_EOF = -2;

        /// OV_HOLE -> -3
        public const int OV_HOLE = -3;

        /// OV_EREAD -> -128
        public const int OV_EREAD = -128;

        /// OV_EFAULT -> -129
        public const int OV_EFAULT = -129;

        /// OV_EIMPL -> -130
        public const int OV_EIMPL = -130;

        /// OV_EINVAL -> -131
        public const int OV_EINVAL = -131;

        /// OV_ENOTVORBIS -> -132
        public const int OV_ENOTVORBIS = -132;

        /// OV_EBADHEADER -> -133
        public const int OV_EBADHEADER = -133;

        /// OV_EVERSION -> -134
        public const int OV_EVERSION = -134;

        /// OV_ENOTAUDIO -> -135
        public const int OV_ENOTAUDIO = -135;

        /// OV_EBADPACKET -> -136
        public const int OV_EBADPACKET = -136;

        /// OV_EBADLINK -> -137
        public const int OV_EBADLINK = -137;

        /// OV_ENOSEEK -> -138
        public const int OV_ENOSEEK = -138;

        /// _OV_FILE_H_ -> 
        /// Error generating expression: Der Wert darf nicht NULL sein.
        ///Parametername: node
        public const string _OV_FILE_H_ = "";

        /// NOTOPEN -> 0
        public const int NOTOPEN = 0;

        /// PARTOPEN -> 1
        public const int PARTOPEN = 1;

        /// OPENED -> 2
        public const int OPENED = 2;

        /// STREAMSET -> 3
        public const int STREAMSET = 3;

        /// INITSET -> 4
        public const int INITSET = 4;

        /// _OV_ENC_H_ -> 
        /// Error generating expression: Der Wert darf nicht NULL sein.
        ///Parametername: node
        public const string _OV_ENC_H_ = "";

        /// OV_ECTL_RATEMANAGE2_GET -> 0x14
        public const int OV_ECTL_RATEMANAGE2_GET = 20;

        /// OV_ECTL_RATEMANAGE2_SET -> 0x15
        public const int OV_ECTL_RATEMANAGE2_SET = 21;

        /// OV_ECTL_LOWPASS_GET -> 0x20
        public const int OV_ECTL_LOWPASS_GET = 32;

        /// OV_ECTL_LOWPASS_SET -> 0x21
        public const int OV_ECTL_LOWPASS_SET = 33;

        /// OV_ECTL_IBLOCK_GET -> 0x30
        public const int OV_ECTL_IBLOCK_GET = 48;

        /// OV_ECTL_IBLOCK_SET -> 0x31
        public const int OV_ECTL_IBLOCK_SET = 49;

        /// OV_ECTL_COUPLING_GET -> 0x40
        public const int OV_ECTL_COUPLING_GET = 64;

        /// OV_ECTL_COUPLING_SET -> 0x41
        public const int OV_ECTL_COUPLING_SET = 65;

        /// OV_ECTL_RATEMANAGE_GET -> 0x10
        public const int OV_ECTL_RATEMANAGE_GET = 16;

        /// OV_ECTL_RATEMANAGE_SET -> 0x11
        public const int OV_ECTL_RATEMANAGE_SET = 17;

        /// OV_ECTL_RATEMANAGE_AVG -> 0x12
        public const int OV_ECTL_RATEMANAGE_AVG = 18;

        /// OV_ECTL_RATEMANAGE_HARD -> 0x13
        public const int OV_ECTL_RATEMANAGE_HARD = 19;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct vorbis_info
    {

        /// int
        public int version;

        /// int
        public int channels;

        /// int
        public int rate;

        /// int
        public int bitrate_upper;

        /// int
        public int bitrate_nominal;

        /// int
        public int bitrate_lower;

        /// int
        public int bitrate_window;

        /// void*
        public IntPtr codec_setup;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct vorbis_dsp_state
    {

        /// int
        public int analysisp;

        /// vorbis_info*
        public IntPtr vi;

        /// float**
        public IntPtr pcm;

        /// float**
        public IntPtr pcmret;

        /// int
        public int pcm_storage;

        /// int
        public int pcm_current;

        /// int
        public int pcm_returned;

        /// int
        public int preextrapolate;

        /// int
        public int eofflag;

        /// int
        public int lW;

        /// int
        public int W;

        /// int
        public int nW;

        /// int
        public int centerW;

        /// ogg_int64_t->__int64
        public long granulepos;

        /// ogg_int64_t->__int64
        public long sequence;

        /// ogg_int64_t->__int64
        public long glue_bits;

        /// ogg_int64_t->__int64
        public long time_bits;

        /// ogg_int64_t->__int64
        public long floor_bits;

        /// ogg_int64_t->__int64
        public long res_bits;

        /// void*
        public IntPtr backend_state;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct vorbis_block
    {

        /// float**
        public IntPtr pcm;

        /// oggpack_buffer->Anonymous_285a1b36_9595_4684_a26e_690f23135134
        public oggpack_buffer opb;

        /// int
        public int lW;

        /// int
        public int W;

        /// int
        public int nW;

        /// int
        public int pcmend;

        /// int
        public int mode;

        /// int
        public int eofflag;

        /// ogg_int64_t->__int64
        public long granulepos;

        /// ogg_int64_t->__int64
        public long sequence;

        /// vorbis_dsp_state*
        public IntPtr vd;

        /// void*
        public IntPtr localstore;

        /// int
        public int localtop;

        /// int
        public int localalloc;

        /// int
        public int totaluse;

        /// alloc_chain*
        public IntPtr reap;

        /// int
        public int glue_bits;

        /// int
        public int time_bits;

        /// int
        public int floor_bits;

        /// int
        public int res_bits;

        /// void*
        public IntPtr @internal;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct alloc_chain
    {

        /// void*
        public IntPtr ptr;

        /// alloc_chain*
        public IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct vorbis_comment
    {

        /// char**
        public IntPtr user_comments;

        /// int*
        public IntPtr comment_lengths;

        /// int
        public int comments;

        /// char*
        public IntPtr vendor;
    }

    /// Return Type: size_t->unsigned int
    ///ptr: void*
    ///size: size_t->unsigned int
    ///nmemb: size_t->unsigned int
    ///datasource: void*
    public delegate uint _read_func(IntPtr ptr, IntPtr size, IntPtr nmemb, IntPtr datasource);

    /// Return Type: int
    ///datasource: void*
    ///offset: ogg_int64_t->__int64
    ///whence: int
    public delegate int _seek_func(IntPtr datasource, long offset, int whence);

    /// Return Type: int
    ///datasource: void*
    public delegate int _close_func(IntPtr datasource);

    /// Return Type: int
    ///datasource: void*
    public delegate int _tell_func(IntPtr datasource);

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct OggVorbis_File
    {

        /// void*
        public IntPtr datasource;

        /// int
        public int seekable;

        /// ogg_int64_t->__int64
        public long offset;

        /// ogg_int64_t->__int64
        public long end;

        /// ogg_sync_state->Anonymous_8020130d_6a89_4f66_8fe6_042783fcb26e
        public ogg_sync_state oy;

        /// int
        public int links;

        /// ogg_int64_t*
        public IntPtr offsets;

        /// ogg_int64_t*
        public IntPtr dataoffsets;

        /// int*
        public IntPtr serialnos;

        /// ogg_int64_t*
        public IntPtr pcmlengths;

        /// vorbis_info*
        public IntPtr vi;

        /// vorbis_comment*
        public IntPtr vc;

        /// ogg_int64_t->__int64
        public long pcm_offset;

        /// int
        public int ready_state;

        /// int
        public int current_serialno;

        /// int
        public int current_link;

        /// double
        public double bittrack;

        /// double
        public double samptrack;

        /// ogg_stream_state->Anonymous_b87fe6ab_9ba1_4af5_a28d_112928b9d5ec
        public ogg_stream_state os;

        /// vorbis_dsp_state
        public vorbis_dsp_state vd;

        /// vorbis_block
        public vorbis_block vb;

        /// ov_callbacks->Anonymous_d6f414e8_3702_4d14_99c9_91cdcb94f508
        public ov_callbacks callbacks;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ovectl_ratemanage_arg
    {

        /// int
        public int management_active;

        /// int
        public int bitrate_hard_min;

        /// int
        public int bitrate_hard_max;

        /// double
        public double bitrate_hard_window;

        /// int
        public int bitrate_av_lo;

        /// int
        public int bitrate_av_hi;

        /// double
        public double bitrate_av_window;

        /// double
        public double bitrate_av_window_center;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ovectl_ratemanage2_arg
    {

        /// int
        public int management_active;

        /// int
        public int bitrate_limit_min_kbps;

        /// int
        public int bitrate_limit_max_kbps;

        /// int
        public int bitrate_limit_reservoir_bits;

        /// double
        public double bitrate_limit_reservoir_bias;

        /// int
        public int bitrate_average_kbps;

        /// double
        public double bitrate_average_damping;
    }

    /// Return Type: void
    ///pcm: float**
    ///channels: int
    ///samples: int
    ///filter_param: void*
    public delegate void Anonymous_84c13e06_455d_4e54_b13e_5306b4d9db9b(ref IntPtr pcm, int channels, int samples, IntPtr filter_param);

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct oggpack_buffer
    {

        /// int
        public int endbyte;

        /// int
        public int endbit;

        /// void*
        public IntPtr buffer;

        /// void*
        public IntPtr ptr;

        /// int
        public int storage;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ogg_sync_state
    {

        /// void*
        public IntPtr data;

        /// int
        public int storage;

        /// int
        public int fill;

        /// int
        public int returned;

        /// int
        public int unsynced;

        /// int
        public int headerbytes;

        /// int
        public int bodybytes;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct ogg_stream_state
    {

        /// void*
        public IntPtr body_data;

        /// int
        public int body_storage;

        /// int
        public int body_fill;

        /// int
        public int body_returned;

        /// int*
        public IntPtr lacing_vals;

        /// ogg_int64_t*
        public IntPtr granule_vals;

        /// int
        public int lacing_storage;

        /// int
        public int lacing_fill;

        /// int
        public int lacing_packet;

        /// int
        public int lacing_returned;

        /// unsigned char[282]
        public fixed byte header[282];

        /// int
        public int header_fill;

        /// int
        public int e_o_s;

        /// int
        public int b_o_s;

        /// int
        public int serialno;

        /// int
        public int pageno;

        /// ogg_int64_t->__int64
        public long packetno;

        /// ogg_int64_t->__int64
        public long granulepos;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ov_callbacks
    {

        /// _read_func
        public _read_func AnonymousMember1;

        /// _seek_func
        public _seek_func AnonymousMember2;

        /// _close_func
        public _close_func AnonymousMember3;

        /// _tell_func
        public _tell_func AnonymousMember4;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ogg_packet
    {

        /// void*
        public IntPtr packet;

        /// int
        public int bytes;

        /// int
        public int b_o_s;

        /// int
        public int e_o_s;

        /// ogg_int64_t->__int64
        public long granulepos;

        /// ogg_int64_t->__int64
        public long packetno;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ogg_iovec_t
    {

        /// void*
        public IntPtr iov_base;

        /// size_t->unsigned int
        public uint iov_len;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ogg_page
    {

        /// void*
        public IntPtr header;

        /// int
        public int header_len;

        /// void*
        public IntPtr body;

        /// int
        public int body_len;
    }

    public partial class NativeMethods
    {

        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writeinit")]
        public static extern void oggpack_writeinit(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writecheck")]
        public static extern int oggpack_writecheck(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writetrunc")]
        public static extern void oggpack_writetrunc(ref oggpack_buffer b, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writealign")]
        public static extern void oggpack_writealign(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///source: void*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writecopy")]
        public static extern void oggpack_writecopy(ref oggpack_buffer b, IntPtr source, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_reset")]
        public static extern void oggpack_reset(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_writeclear")]
        public static extern void oggpack_writeclear(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///buf: void*
        ///bytes: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_readinit")]
        public static extern void oggpack_readinit(ref oggpack_buffer b, IntPtr buf, int bytes);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///value: unsigned int
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_write")]
        public static extern void oggpack_write(ref oggpack_buffer b, uint value, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_look")]
        public static extern int oggpack_look(ref oggpack_buffer b, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_look1")]
        public static extern int oggpack_look1(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_adv")]
        public static extern void oggpack_adv(ref oggpack_buffer b, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_adv1")]
        public static extern void oggpack_adv1(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_read")]
        public static extern int oggpack_read(ref oggpack_buffer b, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_read1")]
        public static extern int oggpack_read1(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_bytes")]
        public static extern int oggpack_bytes(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_bits")]
        public static extern int oggpack_bits(ref oggpack_buffer b);


        /// Return Type: void*
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpack_get_buffer")]
        public static extern IntPtr oggpack_get_buffer(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writeinit")]
        public static extern void oggpackB_writeinit(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writecheck")]
        public static extern int oggpackB_writecheck(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writetrunc")]
        public static extern void oggpackB_writetrunc(ref oggpack_buffer b, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writealign")]
        public static extern void oggpackB_writealign(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///source: void*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writecopy")]
        public static extern void oggpackB_writecopy(ref oggpack_buffer b, IntPtr source, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_reset")]
        public static extern void oggpackB_reset(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_writeclear")]
        public static extern void oggpackB_writeclear(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///buf: void*
        ///bytes: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_readinit")]
        public static extern void oggpackB_readinit(ref oggpack_buffer b, IntPtr buf, int bytes);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///value: unsigned int
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_write")]
        public static extern void oggpackB_write(ref oggpack_buffer b, uint value, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_look")]
        public static extern int oggpackB_look(ref oggpack_buffer b, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_look1")]
        public static extern int oggpackB_look1(ref oggpack_buffer b);


        /// Return Type: void
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_adv")]
        public static extern void oggpackB_adv(ref oggpack_buffer b, int bits);


        /// Return Type: void
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_adv1")]
        public static extern void oggpackB_adv1(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        ///bits: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_read")]
        public static extern int oggpackB_read(ref oggpack_buffer b, int bits);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_read1")]
        public static extern int oggpackB_read1(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_bytes")]
        public static extern int oggpackB_bytes(ref oggpack_buffer b);


        /// Return Type: int
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_bits")]
        public static extern int oggpackB_bits(ref oggpack_buffer b);


        /// Return Type: void*
        ///b: oggpack_buffer*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "oggpackB_get_buffer")]
        public static extern IntPtr oggpackB_get_buffer(ref oggpack_buffer b);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///op: ogg_packet*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_packetin")]
        public static extern int ogg_stream_packetin(ref ogg_stream_state os, ref ogg_packet op);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///iov: ogg_iovec_t*
        ///count: int
        ///e_o_s: int
        ///granulepos: ogg_int64_t->__int64
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_iovecin")]
        public static extern int ogg_stream_iovecin(ref ogg_stream_state os, ref ogg_iovec_t iov, int count, int e_o_s, long granulepos);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_pageout")]
        public static extern int ogg_stream_pageout(ref ogg_stream_state os, ref ogg_page og);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_flush")]
        public static extern int ogg_stream_flush(ref ogg_stream_state os, ref ogg_page og);


        /// Return Type: int
        ///oy: ogg_sync_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_init")]
        public static extern int ogg_sync_init(ref ogg_sync_state oy);


        /// Return Type: int
        ///oy: ogg_sync_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_clear")]
        public static extern int ogg_sync_clear(ref ogg_sync_state oy);


        /// Return Type: int
        ///oy: ogg_sync_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_reset")]
        public static extern int ogg_sync_reset(ref ogg_sync_state oy);


        /// Return Type: int
        ///oy: ogg_sync_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_destroy")]
        public static extern int ogg_sync_destroy(ref ogg_sync_state oy);


        /// Return Type: int
        ///oy: ogg_sync_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_check")]
        public static extern int ogg_sync_check(ref ogg_sync_state oy);


        /// Return Type: char*
        ///oy: ogg_sync_state*
        ///size: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_buffer")]
        public static extern IntPtr ogg_sync_buffer(ref ogg_sync_state oy, int size);


        /// Return Type: int
        ///oy: ogg_sync_state*
        ///bytes: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_wrote")]
        public static extern int ogg_sync_wrote(ref ogg_sync_state oy, int bytes);


        /// Return Type: int
        ///oy: ogg_sync_state*
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_pageseek")]
        public static extern int ogg_sync_pageseek(ref ogg_sync_state oy, ref ogg_page og);


        /// Return Type: int
        ///oy: ogg_sync_state*
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_sync_pageout")]
        public static extern int ogg_sync_pageout(ref ogg_sync_state oy, ref ogg_page og);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_pagein")]
        public static extern int ogg_stream_pagein(ref ogg_stream_state os, ref ogg_page og);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///op: ogg_packet*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_packetout")]
        public static extern int ogg_stream_packetout(ref ogg_stream_state os, ref ogg_packet op);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///op: ogg_packet*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_packetpeek")]
        public static extern int ogg_stream_packetpeek(ref ogg_stream_state os, ref ogg_packet op);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///serialno: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_init")]
        public static extern int ogg_stream_init(ref ogg_stream_state os, int serialno);


        /// Return Type: int
        ///os: ogg_stream_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_clear")]
        public static extern int ogg_stream_clear(ref ogg_stream_state os);


        /// Return Type: int
        ///os: ogg_stream_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_reset")]
        public static extern int ogg_stream_reset(ref ogg_stream_state os);


        /// Return Type: int
        ///os: ogg_stream_state*
        ///serialno: int
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_reset_serialno")]
        public static extern int ogg_stream_reset_serialno(ref ogg_stream_state os, int serialno);


        /// Return Type: int
        ///os: ogg_stream_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_destroy")]
        public static extern int ogg_stream_destroy(ref ogg_stream_state os);


        /// Return Type: int
        ///os: ogg_stream_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_check")]
        public static extern int ogg_stream_check(ref ogg_stream_state os);


        /// Return Type: int
        ///os: ogg_stream_state*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_stream_eos")]
        public static extern int ogg_stream_eos(ref ogg_stream_state os);


        /// Return Type: void
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_checksum_set")]
        public static extern void ogg_page_checksum_set(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_version")]
        public static extern int ogg_page_version(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_continued")]
        public static extern int ogg_page_continued(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_bos")]
        public static extern int ogg_page_bos(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_eos")]
        public static extern int ogg_page_eos(ref ogg_page og);


        /// Return Type: ogg_int64_t->__int64
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_granulepos")]
        public static extern long ogg_page_granulepos(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_serialno")]
        public static extern int ogg_page_serialno(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_pageno")]
        public static extern int ogg_page_pageno(ref ogg_page og);


        /// Return Type: int
        ///og: ogg_page*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_page_packets")]
        public static extern int ogg_page_packets(ref ogg_page og);


        /// Return Type: void
        ///op: ogg_packet*
        [DllImportAttribute("libogg.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ogg_packet_clear")]
        public static extern void ogg_packet_clear(ref ogg_packet op);


        /// Return Type: void
        ///vi: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_info_init")]
        public static extern void vorbis_info_init(ref vorbis_info vi);


        /// Return Type: void
        ///vi: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_info_clear")]
        public static extern void vorbis_info_clear(ref vorbis_info vi);


        /// Return Type: int
        ///vi: vorbis_info*
        ///zo: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_info_blocksize")]
        public static extern int vorbis_info_blocksize(ref vorbis_info vi, int zo);


        /// Return Type: void
        ///vc: vorbis_comment*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_init")]
        public static extern void vorbis_comment_init(ref vorbis_comment vc);


        /// Return Type: void
        ///vc: vorbis_comment*
        ///comment: char*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_add")]
        public static extern void vorbis_comment_add(ref vorbis_comment vc, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string comment);


        /// Return Type: void
        ///vc: vorbis_comment*
        ///tag: char*
        ///contents: char*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_add_tag")]
        public static extern void vorbis_comment_add_tag(ref vorbis_comment vc, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string tag, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string contents);


        /// Return Type: char*
        ///vc: vorbis_comment*
        ///tag: char*
        ///count: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_query")]
        public static extern IntPtr vorbis_comment_query(ref vorbis_comment vc, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string tag, int count);


        /// Return Type: int
        ///vc: vorbis_comment*
        ///tag: char*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_query_count")]
        public static extern int vorbis_comment_query_count(ref vorbis_comment vc, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string tag);


        /// Return Type: void
        ///vc: vorbis_comment*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_clear")]
        public static extern void vorbis_comment_clear(ref vorbis_comment vc);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vb: vorbis_block*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_block_init")]
        public static extern int vorbis_block_init(ref vorbis_dsp_state v, ref vorbis_block vb);


        /// Return Type: int
        ///vb: vorbis_block*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_block_clear")]
        public static extern int vorbis_block_clear(ref vorbis_block vb);


        /// Return Type: void
        ///v: vorbis_dsp_state*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_dsp_clear")]
        public static extern void vorbis_dsp_clear(ref vorbis_dsp_state v);


        /// Return Type: double
        ///v: vorbis_dsp_state*
        ///granulepos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_granule_time")]
        public static extern double vorbis_granule_time(ref vorbis_dsp_state v, long granulepos);


        /// Return Type: char*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_version_string")]
        public static extern IntPtr vorbis_version_string();


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vi: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis_init")]
        public static extern int vorbis_analysis_init(ref vorbis_dsp_state v, ref vorbis_info vi);


        /// Return Type: int
        ///vc: vorbis_comment*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_commentheader_out")]
        public static extern int vorbis_commentheader_out(ref vorbis_comment vc, ref ogg_packet op);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vc: vorbis_comment*
        ///op: ogg_packet*
        ///op_comm: ogg_packet*
        ///op_code: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis_headerout")]
        public static extern int vorbis_analysis_headerout(ref vorbis_dsp_state v, ref vorbis_comment vc, ref ogg_packet op, ref ogg_packet op_comm, ref ogg_packet op_code);


        /// Return Type: float**
        ///v: vorbis_dsp_state*
        ///vals: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis_buffer")]
        public static extern IntPtr vorbis_analysis_buffer(ref vorbis_dsp_state v, int vals);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vals: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis_wrote")]
        public static extern int vorbis_analysis_wrote(ref vorbis_dsp_state v, int vals);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vb: vorbis_block*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis_blockout")]
        public static extern int vorbis_analysis_blockout(ref vorbis_dsp_state v, ref vorbis_block vb);


        /// Return Type: int
        ///vb: vorbis_block*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_analysis")]
        public static extern int vorbis_analysis(ref vorbis_block vb, ref ogg_packet op);


        /// Return Type: int
        ///vb: vorbis_block*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_bitrate_addblock")]
        public static extern int vorbis_bitrate_addblock(ref vorbis_block vb);


        /// Return Type: int
        ///vd: vorbis_dsp_state*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_bitrate_flushpacket")]
        public static extern int vorbis_bitrate_flushpacket(ref vorbis_dsp_state vd, ref ogg_packet op);


        /// Return Type: int
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_idheader")]
        public static extern int vorbis_synthesis_idheader(ref ogg_packet op);


        /// Return Type: int
        ///vi: vorbis_info*
        ///vc: vorbis_comment*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_headerin")]
        public static extern int vorbis_synthesis_headerin(ref vorbis_info vi, ref vorbis_comment vc, ref ogg_packet op);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vi: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_init")]
        public static extern int vorbis_synthesis_init(ref vorbis_dsp_state v, ref vorbis_info vi);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_restart")]
        public static extern int vorbis_synthesis_restart(ref vorbis_dsp_state v);


        /// Return Type: int
        ///vb: vorbis_block*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis")]
        public static extern int vorbis_synthesis(ref vorbis_block vb, ref ogg_packet op);


        /// Return Type: int
        ///vb: vorbis_block*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_trackonly")]
        public static extern int vorbis_synthesis_trackonly(ref vorbis_block vb, ref ogg_packet op);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///vb: vorbis_block*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_blockin")]
        public static extern int vorbis_synthesis_blockin(ref vorbis_dsp_state v, ref vorbis_block vb);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///pcm: float***
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_pcmout")]
        public static extern int vorbis_synthesis_pcmout(ref vorbis_dsp_state v, ref IntPtr pcm);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///pcm: float***
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_lapout")]
        public static extern int vorbis_synthesis_lapout(ref vorbis_dsp_state v, ref IntPtr pcm);


        /// Return Type: int
        ///v: vorbis_dsp_state*
        ///samples: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_read")]
        public static extern int vorbis_synthesis_read(ref vorbis_dsp_state v, int samples);


        /// Return Type: int
        ///vi: vorbis_info*
        ///op: ogg_packet*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_packet_blocksize")]
        public static extern int vorbis_packet_blocksize(ref vorbis_info vi, ref ogg_packet op);


        /// Return Type: int
        ///v: vorbis_info*
        ///flag: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_halfrate")]
        public static extern int vorbis_synthesis_halfrate(ref vorbis_info v, int flag);


        /// Return Type: int
        ///v: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_halfrate_p")]
        public static extern int vorbis_synthesis_halfrate_p(ref vorbis_info v);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_clear")]
        public static extern int ov_clear(ref OggVorbis_File vf);


        /// Return Type: int
        ///path: char*
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_open",CharSet=CharSet.Ansi)]
        public static extern int ov_fopen(string path, ref OggVorbis_File vf);


        /// Return Type: int
        ///datasource: void*
        ///vf: OggVorbis_File*
        ///initial: char*
        ///ibytes: int
        ///callbacks: ov_callbacks->Anonymous_d6f414e8_3702_4d14_99c9_91cdcb94f508
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_open_callbacks")]
        public static extern int ov_open_callbacks(IntPtr datasource, ref OggVorbis_File vf, IntPtr initial, int ibytes, ov_callbacks callbacks);


        /// Return Type: int
        ///datasource: void*
        ///vf: OggVorbis_File*
        ///initial: char*
        ///ibytes: int
        ///callbacks: ov_callbacks->Anonymous_d6f414e8_3702_4d14_99c9_91cdcb94f508
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_test_callbacks")]
        public static extern int ov_test_callbacks(IntPtr datasource, ref OggVorbis_File vf, IntPtr initial, int ibytes, ov_callbacks callbacks);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_test_open")]
        public static extern int ov_test_open(ref OggVorbis_File vf);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///i: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_bitrate")]
        public static extern int ov_bitrate(ref OggVorbis_File vf, int i);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_bitrate_instant")]
        public static extern int ov_bitrate_instant(ref OggVorbis_File vf);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_streams")]
        public static extern int ov_streams(ref OggVorbis_File vf);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_seekable")]
        public static extern int ov_seekable(ref OggVorbis_File vf);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///i: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_serialnumber")]
        public static extern int ov_serialnumber(ref OggVorbis_File vf, int i);


        /// Return Type: ogg_int64_t->__int64
        ///vf: OggVorbis_File*
        ///i: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_raw_total")]
        public static extern long ov_raw_total(ref OggVorbis_File vf, int i);


        /// Return Type: ogg_int64_t->__int64
        ///vf: OggVorbis_File*
        ///i: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_total")]
        public static extern long ov_pcm_total(ref OggVorbis_File vf, int i);


        /// Return Type: double
        ///vf: OggVorbis_File*
        ///i: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_total")]
        public static extern double ov_time_total(ref OggVorbis_File vf, int i);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_raw_seek")]
        public static extern int ov_raw_seek(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_seek")]
        public static extern int ov_pcm_seek(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_seek_page")]
        public static extern int ov_pcm_seek_page(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: double
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_seek")]
        public static extern int ov_time_seek(ref OggVorbis_File vf, double pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: double
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_seek_page")]
        public static extern int ov_time_seek_page(ref OggVorbis_File vf, double pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_raw_seek_lap")]
        public static extern int ov_raw_seek_lap(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_seek_lap")]
        public static extern int ov_pcm_seek_lap(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: ogg_int64_t->__int64
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_seek_page_lap")]
        public static extern int ov_pcm_seek_page_lap(ref OggVorbis_File vf, long pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: double
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_seek_lap")]
        public static extern int ov_time_seek_lap(ref OggVorbis_File vf, double pos);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pos: double
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_seek_page_lap")]
        public static extern int ov_time_seek_page_lap(ref OggVorbis_File vf, double pos);


        /// Return Type: ogg_int64_t->__int64
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_raw_tell")]
        public static extern long ov_raw_tell(ref OggVorbis_File vf);


        /// Return Type: ogg_int64_t->__int64
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_pcm_tell")]
        public static extern long ov_pcm_tell(ref OggVorbis_File vf);


        /// Return Type: double
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_time_tell")]
        public static extern double ov_time_tell(ref OggVorbis_File vf);


        /// Return Type: vorbis_info*
        ///vf: OggVorbis_File*
        ///link: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_info")]
        public static extern IntPtr ov_info(ref OggVorbis_File vf, int link);


        /// Return Type: vorbis_comment*
        ///vf: OggVorbis_File*
        ///link: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_comment")]
        public static extern IntPtr ov_comment(ref OggVorbis_File vf, int link);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///pcm_channels: float***
        ///samples: int
        ///bitstream: int*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_read_float")]
        public static extern int ov_read_float(ref OggVorbis_File vf, ref IntPtr pcm_channels, int samples, ref int bitstream);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///buffer: char*
        ///length: int
        ///bigendianp: int
        ///word: int
        ///sgned: int
        ///bitstream: int*
        ///filter: Anonymous_84c13e06_455d_4e54_b13e_5306b4d9db9b
        ///filter_param: void*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_read_filter")]
        public static extern int ov_read_filter(ref OggVorbis_File vf, IntPtr buffer, int length, int bigendianp, int word, int sgned, ref int bitstream, Anonymous_84c13e06_455d_4e54_b13e_5306b4d9db9b filter, IntPtr filter_param);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///buffer: char*
        ///length: int
        ///bigendianp: int
        ///word: int
        ///sgned: int
        ///bitstream: int*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_read")]
        public static extern int ov_read(ref OggVorbis_File vf, byte[] buffer, int length, int bigendianp, int word, int sgned, ref int bitstream);


        /// Return Type: int
        ///vf1: OggVorbis_File*
        ///vf2: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_crosslap")]
        public static extern int ov_crosslap(ref OggVorbis_File vf1, ref OggVorbis_File vf2);


        /// Return Type: int
        ///vf: OggVorbis_File*
        ///flag: int
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_halfrate")]
        public static extern int ov_halfrate(ref OggVorbis_File vf, int flag);


        /// Return Type: int
        ///vf: OggVorbis_File*
        [DllImportAttribute("libvorbisfile.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ov_halfrate_p")]
        public static extern int ov_halfrate_p(ref OggVorbis_File vf);


        /// Return Type: int
        ///vi: vorbis_info*
        ///channels: int
        ///rate: int
        ///max_bitrate: int
        ///nominal_bitrate: int
        ///min_bitrate: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_init")]
        public static extern int vorbis_encode_init(ref vorbis_info vi, int channels, int rate, int max_bitrate, int nominal_bitrate, int min_bitrate);


        /// Return Type: int
        ///vi: vorbis_info*
        ///channels: int
        ///rate: int
        ///max_bitrate: int
        ///nominal_bitrate: int
        ///min_bitrate: int
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_setup_managed")]
        public static extern int vorbis_encode_setup_managed(ref vorbis_info vi, int channels, int rate, int max_bitrate, int nominal_bitrate, int min_bitrate);


        /// Return Type: int
        ///vi: vorbis_info*
        ///channels: int
        ///rate: int
        ///quality: float
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_setup_vbr")]
        public static extern int vorbis_encode_setup_vbr(ref vorbis_info vi, int channels, int rate, float quality);


        /// Return Type: int
        ///vi: vorbis_info*
        ///channels: int
        ///rate: int
        ///base_quality: float
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_init_vbr")]
        public static extern int vorbis_encode_init_vbr(ref vorbis_info vi, int channels, int rate, float base_quality);


        /// Return Type: int
        ///vi: vorbis_info*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_setup_init")]
        public static extern int vorbis_encode_setup_init(ref vorbis_info vi);


        /// Return Type: int
        ///vi: vorbis_info*
        ///number: int
        ///arg: void*
        [DllImportAttribute("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_encode_ctl")]
        public static extern int vorbis_encode_ctl(ref vorbis_info vi, int number, IntPtr arg);

    }
}