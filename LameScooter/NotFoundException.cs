using System;

namespace LameScooter{
    public class NotFoundException : Exception{
        public NotFoundException(){
        }

        public NotFoundException(string argument) : base(){
        }
    }
}