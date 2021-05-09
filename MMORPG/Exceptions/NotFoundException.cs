using System;

namespace MMORPG.Exceptions{
    public class NotFoundException : Exception{
        public override string Message{ get; }

        public NotFoundException(){
            this.Message = "";
        }

        public NotFoundException(string message) : base(){
            this.Message = message;
        }
    }
}