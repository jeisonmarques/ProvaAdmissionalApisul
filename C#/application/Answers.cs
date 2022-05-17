
namespace OakClass
{
    public class Answers
    {
        private int andar;
        private char elevador;
        private char turno;

        

        public Answers(int andar, char elevador, char turno)
        {
            this.andar = andar;
            this.elevador = elevador;
            this.turno = turno;
        }

        public int Andar{
            get{ return this.andar; }
        }
        public char Elevador{
            get{ return this.elevador; }
        }
        public char Turno{
            get{ return this.turno; }
        }

        public override string ToString()
        {
            return $"Andar: {this.andar} \nElevador: {this.elevador} \nTurno: {this.turno}";
        }
    }
}
