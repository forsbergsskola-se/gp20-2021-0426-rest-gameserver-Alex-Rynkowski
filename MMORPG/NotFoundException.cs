using System;

namespace MMORPG{
    public class NotFoundException : Exception{
        public NotFoundException(){
        }

        public NotFoundException(string message) : base(){
        }
    }
}