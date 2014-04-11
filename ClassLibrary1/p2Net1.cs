using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using Arma2Net.AddInProxy;

namespace p2Net1
{
    [AddIn("p2Net1")] //Name of Function
    public class p2Net1 : MethodAddIn
    {
       public Result readGold (string args)
        {
            /*	Write
            Inputs: UID, GoldAmount
            Outputs: New Gold Amount
            Description: Writes player currency ammount DB
            PS: if newgold < 0, set gold to 0
            */
            string _url;
            string _armaResponse;

            _url = string.Format("http://192.223.27.54/1/bank/bank.php?uid={0}&process=Balance&gold=0&gold10oz=0&briefcase=0", args);

            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_url);
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                Stream _receiveStream = _response.GetResponseStream();
                StreamReader _readStream = null;
                if (_response.CharacterSet == null)
                    _readStream = new StreamReader(_receiveStream);
                else
                    _readStream = new StreamReader(_receiveStream, Encoding.GetEncoding(_response.CharacterSet));
                string _data = _readStream.ReadToEnd();
                _response.Close();
                _readStream.Close();
                _armaResponse = _data;
                //_armaResponse = "working";

            }
            else
            {
                _armaResponse = "[false,'Error In External DLL Extension']";
            }
            return new Result(@_armaResponse);


            //code here
            //return "WriteGold: This function is not ready yet.";
        }

       public Result writeGold(string uid, string gold, string gold10z, string briefcase)
       {
           /*	Write
           Inputs: UID, GoldAmount
           Outputs: New Gold Amount
           Description: Writes player currency ammount DB
           PS: if newgold < 0, set gold to 0
           */
           string _url;
           string _armaResponse;


           _url = string.Format("http://192.223.27.54/1/bank/bank.php?uid={0}&process=Deposit&gold={1}&gold10oz={2}&briefcase={3}", uid, gold, gold10z, briefcase);

           HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_url);
           HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
           if (_response.StatusCode == HttpStatusCode.OK)
           {
               Stream _receiveStream = _response.GetResponseStream();
               StreamReader _readStream = null;
               if (_response.CharacterSet == null)
                   _readStream = new StreamReader(_receiveStream);
               else
                   _readStream = new StreamReader(_receiveStream, Encoding.GetEncoding(_response.CharacterSet));
               string _data = _readStream.ReadToEnd();
               _response.Close();
               _readStream.Close();
               _armaResponse = _data;
               //_armaResponse = "working";

           }
           else
           {
               _armaResponse = "[false,'Error In External DLL Extension']";
           }
           return new Result(@_armaResponse);


           //code here
           //return "WriteGold: This function is not ready yet.";


       }

       public Result writeLog(string logName, string logMessage)
       {
           /*	Write to Log:
           Inputs: logName, logMessage
           Outputs: C:\p2Arma2Net\Logs\logName.txt
           Description: Writes logMessage to logName.txt
           */
           System.IO.StreamWriter logFile =
            new System.IO.StreamWriter("X:\\p2Arma2Net\\Logs\\" + logName + ".txt", true);
           logFile.WriteLine(logMessage + "\r");
           logFile.Close();
           string _armaResponse;
           _armaResponse = "100";
           return new Result(@_armaResponse);
       }

        public Result readText(string fileName, string startOfLineReplacement, string newLineReplacement)
        {
            /* Read from File:
             * Inputs: File Name, End of line char replacement, Start of line character replacment,
             * Input Path: C:\p2Arma2Net\toRead\*
             * Outputs: Text from file
             * Description: Useful for sending information live to a server without a restart.
             */
            string _armaResponse = "";

            System.IO.StreamReader streamReader =
            new System.IO.StreamReader("X:\\p2Arma2Net\\toRead\\" + fileName + ".txt", true);

            while (true)
            {
                string currentLine;
                currentLine = streamReader.ReadLine();
                if (currentLine == null)
                {
                    _armaResponse = _armaResponse + "\r" + currentLine;
                    break;
                }
                else
                {
                    if (startOfLineReplacement != "none")
                    {
                       currentLine =  startOfLineReplacement + currentLine;
                    }
                    if (newLineReplacement != "none")
                    {
                        currentLine = currentLine + newLineReplacement;
                    }
                    _armaResponse = _armaResponse + "\r" + currentLine;
                }
            }
            streamReader.Close();
            return new Result(@_armaResponse);
        }
    }


    public class Result
    {
        private readonly string _answer;

        public Result(string answer)
        {
            _answer = answer;
        }

        public override string ToString()
        {
            return _answer;
        }
    }
}
