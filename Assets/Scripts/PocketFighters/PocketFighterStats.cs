using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PocketFighters
{
    public class PocketFighterStats : MonoBehaviour
    {
        // Start is called before the first frame update.
        public EstiloLuta estiloLuta;
        public int forca, resistencia, vida, vidaMax;
        public int tecnica, agilidade;
        public int experiência;
        
        public int Forca { get { return this.forca; } set { this.forca = value; } }
        public int Resistencia { get { return this.resistencia; } set { this.resistencia = value; } }
        public int Vida
        {
            get { return this.vida; }
            set
            {
                this.vida = value;
                if (this.vida <= 0) this.vida = 0;
                else if (this.vida > this.vidaMax) this.vida = this.vidaMax;
            }
        }

        public int VidaMax
        {
            get { return this.vidaMax; }
            set
            {
                this.vidaMax = value;
                if (this.vidaMax == 0) this.vidaMax = 10;
                this.Vida = this.vidaMax;
            }
        }

        public int Tecnica { get { return this.tecnica; } set { this.tecnica = value; } }

        public int Agilidade { get { return this.agilidade; } set { this.agilidade = value; } }

        public void Start()
        {
        }

        public void Update()
        {

        }

        public void ClearStats()
        {
            forca = resistencia = tecnica = agilidade = 1;
            vida = VidaMax = 10;
        }
    }
}
