using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Filters
{
    /// <summary>
    /// Filter to gzip/deflate responses for the decorated controller/action(s).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [OrchardFeature("CSM.WebApi")]
    public class CompressResponseAttribute : ActionFilterAttribute
    {
        private enum Encodings
        {
            Unsupported,
            GZip,
            Deflate
        }

        private static readonly Dictionary<Encodings, string> encodings =
                            new Dictionary<Encodings, string>
        {
            { Encodings.GZip, "gzip" },
            { Encodings.Deflate, "deflate" }
        };

        private static readonly Dictionary<Encodings, Func<Stream, dynamic>> compressors =
                            new Dictionary<Encodings, Func<Stream, dynamic>>
        {
            { Encodings.GZip, (stream) => new GZipStream(stream, CompressionMode.Compress, false) },
            { Encodings.Deflate, (stream) => new DeflateStream(stream, CompressionMode.Compress, false) }
        };

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            var request = actionExecutedContext.Request;
            var response = actionExecutedContext.Response;
            var compressionSupport = getCompressionSupport(request);

            compressResponse(response, compressionSupport);
        }

        private static Encodings getCompressionSupport(HttpRequestMessage request)
        {
            var acceptEncoding = request.Headers.AcceptEncoding;

            if (acceptEncoding != null)
            {
                foreach (var kvp in encodings)
                {
                    if(acceptEncoding.Any(header => header.Value.Contains(kvp.Value)))
                    {
                        return kvp.Key;
                    }
                }
            }

            return Encodings.Unsupported;
        }

        private static void compressResponse(HttpResponseMessage response, Encodings encoding)
        {
            if (encoding == Encodings.Unsupported)
                return;

            var originalContent = response.Content;
            var bytes = originalContent == null ? null : originalContent.ReadAsByteArrayAsync().Result;
            var compressedContent = bytes == null ? new byte[0] : compressBytes(bytes, encoding);

            response.Content = new ByteArrayContent(compressedContent);

            foreach (var header in originalContent.Headers)
            {
                response.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            response.Content.Headers.ContentEncoding.Clear();
            response.Content.Headers.ContentEncoding.Add(encodings[encoding]);
        }

        private static byte[] compressBytes(byte[] bytes, Encodings encoding)
        {
            if (bytes == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                dynamic compressor = compressors[encoding](output);

                compressor.Write(bytes, 0, bytes.Length);

                return output.ToArray();
            }
        }
    }
}