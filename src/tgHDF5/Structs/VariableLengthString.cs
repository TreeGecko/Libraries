using System;
using System.Runtime.InteropServices;

namespace TreeGecko.Library.HDF5.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VariableLengthString
    {
        /// <summary>
        /// Pointer to the variable length string
        /// </summary>
        private char* recordedText;

        /// <summary>
        /// Gets or sets the pointer to the variable length string.
        /// </summary>
        [CLSCompliant(false)]
        public char* RecordedText
        {
            get
            {
                return this.recordedText;
            }

            set
            {
                this.recordedText = value;
            }
        }

        /// <summary>
        /// Returns a System.String that represents the current System.Object.
        /// </summary>
        /// <returns>The value of the variable length string.</returns>
        public override string ToString()
        {
            string s;

            // The HDF5 STRING is not a string but in fact a char*
            // Therefore, we need to translate the return into a pointer address
            IntPtr ipp = (IntPtr)this.recordedText;

            // This call is used to transform the pointer into the value of the pointer.
            // NOTE:  this only works with null-terminated strings.
            s = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ipp);

            //// FREE THE MEMORY TO THE POINTER??
            //// System.Runtime.InteropServices.Marshal.FreeHGlobal(ipp);

            return s;
        }
    }
}
