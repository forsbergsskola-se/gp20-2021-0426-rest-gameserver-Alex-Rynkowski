using System;

namespace MMORPG.Exceptions{
    public class NoQuestFoundException : Exception{
        public override string Message{ get; }

        public NoQuestFoundException(){
        }

        public NoQuestFoundException(string message) : base(){
            this.Message = message;
        }
    }
}