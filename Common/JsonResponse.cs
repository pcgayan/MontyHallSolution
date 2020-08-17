using System;
using System.Globalization;
using System.Net;

namespace Common
{
    public class JsonResponse
    {
        public readonly int id;
        public readonly int stage;
        public readonly String status;
        public readonly String message;
        public readonly String data;

        public JsonResponse(int id, int stage, String status, String message, String data)
        {
            this.id = id;
            this.stage = stage;
            this.status = status;
            this.message = message;
            this.data = data;
        }

    }
}
