using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ivi.Visa.Interop; // SCPI functions
using System.Threading;

namespace DTV_setting
{
        
    public partial class DTV_Form : Form
    {
        private Ivi.Visa.Interop.FormattedIO488 ioTestSet;
        public bool bDTVON = false;
        
        public DTV_Form()
        {            
            InitializeComponent();
        }


        private void DTV_setting(object sender, EventArgs e)
        {
            ioTestSet = new FormattedIO488();
        }

        private void btSetDTV_Click(object sender, EventArgs e)
        {
            string strCommand = string.Empty;
            string strReturn = string.Empty;

            //CMW500
            try
            {
                ResourceManager grm = new ResourceManager();
                ioTestSet.IO = (IMessage)grm.Open("RS_CMW500", AccessMode.NO_LOCK, 2000, "");             
            }
            catch
            {
                ioTestSet.IO = null;                
            }


            if ((ioTestSet != null) && (!bDTVON))
            {
                this.txtSCPICommands.Clear();

                if ((txtAmplitude.Text == "") || (Convert.ToDouble(txtAmplitude.Text) > - 10))
                {
                    MessageBox.Show("Ajuste a amplitude entre -10 a -65 dBm");
                    return;
                }

                if ((txtFrequency.Text == "") || (Convert.ToDouble(txtFrequency.Text) > 803.143) || (Convert.ToDouble(txtFrequency.Text) < 473.143))
                {
                    MessageBox.Show("Ajuste a Frequencia entre 473.143 a 803.143 MHz");
                    return;
                }
                
                this.txtSCPICommands.Text = "-> *IDN? \r\n";
                ioTestSet.WriteString("*IDN?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                //Verify if DTV wave form can be found                
                this.txtSCPICommands.Text += "-> MMEM:CAT? 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\'" + "\r\n";
                ioTestSet.WriteString("MMEM:CAT? 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\'", true);
                strReturn = ioTestSet.ReadString();
                this.txtSCPICommands.Text += "<-" + strReturn + "\r\n";

                if (strReturn.Contains("ISDB-Tb_Digital_TV_withPR.wv") == false)
                {
                    MessageBox.Show("Wave Form: \r\n D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\ISDB-Tb_Digital_TV_withPR.wv \r\n Não encontrada !!!","Wave Form Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                this.txtSCPICommands.Text += "-> SOUR:GPRF:GEN:ARB:FILE 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\ISDB-Tb_Digital_TV_withPR.wv';*OPC? \r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN:ARB:FILE 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\ISDB-Tb_Digital_TV_withPR.wv';*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SOUR:GPRF:GEN:ARB:FILE?" + "\r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN:ARB:FILE?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SOUR:GPRF:GEN:BBM ARB;:SOUR:GPRF:GEN:ARB:REP CONT;:SOUR:GPRF:GEN:LIST OFF;:TRIG:GPRF:GEN:ARB:RETR ON;:TRIG:GPRF:GEN:ARB:AUT ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN:BBM ARB;:SOUR:GPRF:GEN:ARB:REP CONT;:SOUR:GPRF:GEN:LIST OFF;:TRIG:GPRF:GEN:ARB:RETR ON;:TRIG:GPRF:GEN:ARB:AUT ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SYST:ERR?" + "\r\n";
                ioTestSet.WriteString("SYST:ERR?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "*CLS" + "\r\n";
                ioTestSet.WriteString("*CLS", true);

                this.txtSCPICommands.Text += "-> " + "CONFigure:FDCorrection:USAGe? RF1C" + "\r\n";
                ioTestSet.WriteString("CONFigure:FDCorrection:USAGe? RF1C", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                //Set frequency
                strCommand = "SOUR:GPRF:GEN1:RFS:FREQ " + txtFrequency.Text.ToString() + " MHz;*OPC?";
                this.txtSCPICommands.Text += "-> " + strCommand + "\r\n";
                ioTestSet.WriteString(strCommand, true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SOUR:GPRF:GEN1:STAT ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                do
                {
                    this.txtSCPICommands.Text += "-> " + "SOUR:GPRF:GEN1:STAT?" + "\r\n";
                    ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT?", true);
                    strReturn = ioTestSet.ReadString();
                    this.txtSCPICommands.Text += "<-" + strReturn + "\r\n";
                }
                while (strReturn.Contains("OFF") == true);
                	
                strCommand = "SOUR:GPRF:GEN1:RFS:LEV " + txtAmplitude.Text.ToString() + " dBm;*OPC?";
                this.txtSCPICommands.Text += "-> " + strCommand + "\r\n";
                ioTestSet.WriteString(strCommand, true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                bDTVON = true;
                this.btSetDTV.Text = "SET DTV - OFF";
            }
            else
            {
                this.txtSCPICommands.Clear();
                this.txtSCPICommands.Text += "-> " + "SOUR:GPRF:GEN1:STAT OFF;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT OFF;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                bDTVON = false;
                this.btSetDTV.Text = "SET DTV - ON";
            }
        }

        private void btEXMSetDTV_Click(object sender, EventArgs e)
        {
            string strCommand = string.Empty;
            string strReturn = string.Empty;

            //EXM
            try
            {
                ResourceManager grm = new ResourceManager();
                ioTestSet.IO = (IMessage)grm.Open("AGILENT_EXT", AccessMode.NO_LOCK, 2000, "");
            }
            catch
            {
                ioTestSet.IO = null;
            }


            if ((ioTestSet != null) && (!bDTVON))
            {
                this.txtSCPICommands.Clear();

                if ((txtAmplitude.Text == "") || (Convert.ToDouble(txtAmplitude.Text) > -10))
                {
                    MessageBox.Show("Ajuste a amplitude entre -10 a -65 dBm");
                    return;
                }

                if ((txtFrequency.Text == "") || (Convert.ToDouble(txtFrequency.Text) > 803.143) || (Convert.ToDouble(txtFrequency.Text) < 473.143))
                {
                    MessageBox.Show("Ajuste a Frequencia entre 473.143 a 803.143 MHz");
                    return;
                }

                this.txtSCPICommands.Text = "-> *IDN? \r\n";
                ioTestSet.WriteString("*IDN?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "->" + "FEED:RF:PORT:OUTP RFIO2;*OPC?" + "\r\n";
                ioTestSet.WriteString("FEED:RF:PORT:OUTP RFIO2;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "FEED:RF:PORT:INPUT RFIO2;*OPC?" + "\r\n";
                ioTestSet.WriteString("FEED:RF:PORT:INPUT RFIO2;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SOUR:AM:STAT OFF;:SOUR:FM:STAT OFF;:SOUR:PM:STAT OFF;:OUTP:MOD ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:AM:STAT OFF;:SOUR:FM:STAT OFF;:SOUR:PM:STAT OFF;:OUTP:MOD ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);


                this.txtSCPICommands.Text += "-> " + "*CLS" + "\r\n";
                ioTestSet.WriteString("*CLS", true);
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SYST:ERR?" + "\r\n";
                ioTestSet.WriteString("SYST:ERR?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:STAT OFF;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:STAT OFF;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:CAT?" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:CAT?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:WAV 'ISDB-Tb_Digital_TV_HD.wfm';*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:WAV 'ISDB-Tb_Digital_TV_HD.wfm';*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SYST:ERR?" + "\r\n";
                ioTestSet.WriteString("SYST:ERR?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:TRIG:TYPE CONT" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:TRIG:TYPE CONT", true);
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:TRIG:TYPE:CONT FREE" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:TRIG:TYPE:CONT FREE", true);
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SOUR:RAD:ARB:STAT ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:RAD:ARB:STAT ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "SYST:ERR?" + "\r\n";
                ioTestSet.WriteString("SYST:ERR?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:DESC?" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:DESC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:DEL;*OPC?" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:DEL;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:DESC 'Rx RF IO2'" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:DESC 'Rx RF IO2'", true);
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:STAT ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:STAT ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:DATA:MERGE 587142857,1.75;*OPC?" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:DATA:MERGE 587142857,1.75;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                this.txtSCPICommands.Text += "-> " + "CORR:CSET4:COMM 'UUT_RF_CONN2_TO_TESTSET_ALT1_RF';*OPC?" + "\r\n";
                ioTestSet.WriteString("CORR:CSET4:COMM 'UUT_RF_CONN2_TO_TESTSET_ALT1_RF';*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                //Set frequency
                strCommand = "SOUR:FREQ " + txtFrequency.Text.ToString() + " MHz;*OPC?";
                this.txtSCPICommands.Text += "-> " + strCommand + "\r\n";
                ioTestSet.WriteString(strCommand, true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);
                this.txtSCPICommands.Text += "-> " + "OUTP ON;*OPC?" + "\r\n";
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT ON;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";
                Thread.Sleep(100);

                do
                {
                    this.txtSCPICommands.Text += "-> " + "OUTP?" + "\r\n";
                    ioTestSet.WriteString("OUTP?", true);
                    strReturn = ioTestSet.ReadString();
                    this.txtSCPICommands.Text += "<-" + strReturn + "\r\n";
                }
                while (strReturn.Contains("OFF") == true);

                strCommand = "SOUR:POW " + txtAmplitude.Text.ToString() + " dBm;*OPC?";
                this.txtSCPICommands.Text += "-> " + strCommand + "\r\n";
                ioTestSet.WriteString(strCommand, true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                bDTVON = true;
                this.btEXMSetDTV.Text = "SET DTV - OFF";
            }
            else
            {
                this.txtSCPICommands.Clear();
                this.txtSCPICommands.Text += "-> " + "OUTP OFF;*OPC?" + "\r\n";
                ioTestSet.WriteString("OUTP OFF;*OPC?", true);
                this.txtSCPICommands.Text += "<-" + ioTestSet.ReadString() + "\r\n";

                bDTVON = false;
                this.btEXMSetDTV.Text = "SET DTV - ON";
            }
        }

        
     
    }

     
}
