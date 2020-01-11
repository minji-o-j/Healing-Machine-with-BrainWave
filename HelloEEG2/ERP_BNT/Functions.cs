using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Timers;
namespace ERP_BNT
{
    class Functions
    {
        SoundPlayer simpleSound;
        public void playOrdiSound()
        {
            simpleSound = new SoundPlayer(@"C:\Users\dbtjq\Documents\visual studio 2015\Projects\HelloEEG\ERP_BNT\Sound\Orginal.wav");
            simpleSound.Play();
        }

        public void playStimulSound()
        {
            simpleSound = new SoundPlayer(@"C:\Users\dbtjq\Documents\visual studio 2015\Projects\HelloEEG\ERP_BNT\Sound\Stimuli.wav");
            simpleSound.Play();
        }
    }
}
