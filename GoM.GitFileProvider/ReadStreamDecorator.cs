using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider
{
    class ReadStreamDecorator : Stream
    {
        Stream _stream;
        RepositoryWrapper _rw;

        public ReadStreamDecorator(Stream stream, RepositoryWrapper rw)
        {
            _stream = stream;
            _rw = rw;
            _rw.StreamWrapperCount++;
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position { get => _stream.Position; set => _stream.Position = value; }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        new public void Dispose()
        {
            _rw.StreamWrapperCount--;
            _rw.Dispose();
        }
    }
}
