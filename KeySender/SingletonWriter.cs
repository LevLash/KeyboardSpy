using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySender
{
    public class SingletonWriter
    {
        private StreamWriter writer;
        private static SingletonWriter instance;

        private SingletonWriter(StreamWriter w)
        {
            this.writer = w;
        }

        public static SingletonWriter CreateInstance(StreamWriter w)
        {
            if (instance == null)
            {
                instance = new SingletonWriter(w);
            }

            return instance;
        }

        public static SingletonWriter Instance { 
            get
            {
                if (instance == null)
                {
                    throw new NullReferenceException("instance == null");
                }
                return instance;
            }
        }

        public async void Write(string a)
        {
            try
            {
                await writer.WriteAsync(a);
            }
            catch 
            {

           }
        }
    }
}
