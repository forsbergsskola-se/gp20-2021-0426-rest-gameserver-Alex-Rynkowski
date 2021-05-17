using System;

namespace MMORPG.Exceptions{
    public class PlayerException : Exception{
        public override string Message{ get; }

        public PlayerException(){
        }

        public PlayerException(string message) : base(message){
            this.Message = message;
        }
    }
}