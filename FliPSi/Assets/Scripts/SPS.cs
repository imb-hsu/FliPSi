using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

public class SPS : MonoBehaviour
{
    public string sender_filename = Path.Combine(Environment.CurrentDirectory, "UnityToCodesys");
    public string sender_IPAdress = "255.255.255.255";
    public string sender_Port = "1202";
    public string sender_ListID = "1";
    public string receiver_filename = Path.Combine(Environment.CurrentDirectory, "CodesysToUnity");
    public string receiver_IPAdress = "255.255.255.255";
    public string receiver_Port = "1203";
    public string receiver_ListID = "2";
    private NetVarList nvl_sender = new NetVarList();
    private NetVarList nvl_receiver = new NetVarList();

    private void Awake()
    {
        nvl_sender.IPAdress = sender_IPAdress;
        nvl_sender.Port = int.Parse(sender_Port);
        nvl_sender.ListID = int.Parse(sender_ListID);
        nvl_receiver.IPAdress = receiver_IPAdress;
        nvl_receiver.Port = int.Parse(receiver_Port);
        nvl_receiver.ListID = int.Parse(receiver_ListID);
    }
    #region add, send, remove methods
    public void AddSenderItem(string name, DataType dataType)
    {
        CData item = new CData(name, dataType);
        nvl_sender.CDataList.Add(item);
        nvl_sender.ExportGVLFile(sender_filename);
    }

    public void AddSenderItem(string name, int fieldLength, DataType dataType)
    {
        CData item = new CData(name, fieldLength, dataType);
        nvl_sender.CDataList.Add(item);
        nvl_sender.ExportGVLFile(sender_filename);
    }

    public void AddSenderItem(string name, DataType dataType, object value)
    {
        CData item = new CData(name, dataType, value);
        nvl_sender.CDataList.Add(item);
        nvl_sender.ExportGVLFile(sender_filename);
    }

    public void AddSenderItem(string name, int fieldLength, DataType dataType, object value)
    {
        CData item = new CData(name, fieldLength, dataType, value);
        nvl_sender.CDataList.Add(item);
        nvl_sender.ExportGVLFile(sender_filename);
    }

    public void AddReceiverItem(string name, DataType dataType)
    {
        CData item = new CData(name, dataType);
        nvl_receiver.CDataList.Add(item);
        nvl_receiver.ExportGVLFile(receiver_filename);
    }

    public void AddReceiverItem(string name, int fieldLength, DataType dataType)
    {
        CData item = new CData(name, fieldLength, dataType);
        nvl_receiver.CDataList.Add(item);
        nvl_receiver.ExportGVLFile(receiver_filename);
    }

    public void AddReceiverItem(string name, DataType dataType, object value)
    {
        CData item = new CData(name, dataType, value);
        nvl_receiver.CDataList.Add(item);
        nvl_receiver.ExportGVLFile(receiver_filename);
    }

    public void AddReceiverItem(string name, int fieldLength, DataType dataType, object value)
    {
        CData item = new CData(name, fieldLength, dataType, value);
        nvl_receiver.CDataList.Add(item);
        nvl_receiver.ExportGVLFile(receiver_filename);
    }

    public void RemoveSenderItem(string name)
    {
        nvl_sender.CDataList.Remove(nvl_sender.CDataList.Find(x => x.VariableName == name));
        nvl_sender.ExportGVLFile(sender_filename);
    }

    public void RemoveReceiverItem(string name)
    {
        nvl_receiver.CDataList.Remove(nvl_receiver.CDataList.Find(x => x.VariableName == name));
        nvl_receiver.ExportGVLFile(receiver_filename);
    }
    #endregion

    public object ReturnValueByName(string name)
    {
        if (nvl_receiver.CDataList.Exists(x => x.VariableName == name))
            return nvl_receiver.CDataList.Find(x => x.VariableName == name).Value;
        else
            return null;
    }

    public void ChangeValueByName(string name, object value)
    {
        if (nvl_sender.CDataList.Exists(x => x.VariableName == name))
        {
            nvl_sender.CDataList.Find(x => x.VariableName == name).Value = value;
            return;
        }
        else
            return;
    }

    private bool start = false;
    public void Run(bool run)
    {
        start = run;
    }
    private void FixedUpdate()
    {
        if (start)
        {
            nvl_sender.SendValues();
            nvl_receiver.ReadValues();
        }
    }

    #region Menü

    bool optionWindow = false;
    // controll options window
    private void OnGUI()
    {
        if (optionWindow)
        {
            GUI.Window(0, new Rect(0, 0, 400, 330), OptionWindow, "Options");
        }
    }

    // Options menu
    void OptionWindow(int id)
    {
        GUI.Label(new Rect(20, 30, 100, 20), "Sender Adress");
        sender_IPAdress = GUI.TextField(new Rect(200, 30, 130, 20), sender_IPAdress, 15);

        GUI.Label(new Rect(20, 60, 100, 20), "Sender Port");
        sender_Port = GUI.TextField(new Rect(200, 60, 50, 20), sender_Port, 5);

        GUI.Label(new Rect(20, 90, 150, 20), "Sender ID");
        sender_ListID = GUI.TextField(new Rect(200, 90, 50, 20), sender_ListID, 2);

        GUI.Label(new Rect(20, 120, 150, 20), "Sender directory");
        sender_filename = GUI.TextField(new Rect(200, 120, 130, 20), sender_filename);

        GUI.Label(new Rect(20, 160, 100, 20), "receiver Adress");
        receiver_IPAdress = GUI.TextField(new Rect(200, 160, 130, 20), receiver_IPAdress, 15);

        GUI.Label(new Rect(20, 190, 100, 20), "receiver Port");
        receiver_Port = GUI.TextField(new Rect(200, 190, 50, 20), receiver_Port, 15);

        GUI.Label(new Rect(20, 220, 180, 20), "receiver ID");
        receiver_ListID = GUI.TextField(new Rect(200, 220, 50, 20), receiver_ListID, 2);

        GUI.Label(new Rect(20, 250, 150, 20), "receiver directory");
        sender_filename = GUI.TextField(new Rect(200, 250, 130, 20), sender_filename);

        // destroy GO
        if (GUI.Button(new Rect(130, 290, 70, 20), "Destroy"))
        {
            Destroy(this.gameObject);
        }

        // close window
        if (GUI.Button(new Rect(200, 290, 50, 20), "OK"))
        {
            optionWindow = false;

            nvl_sender.IPAdress = sender_IPAdress;
            nvl_sender.Port = int.Parse(sender_Port);
            nvl_sender.ListID = int.Parse(sender_ListID);
            nvl_receiver.IPAdress = receiver_IPAdress;
            nvl_receiver.Port = int.Parse(receiver_Port);
            nvl_receiver.ListID = int.Parse(receiver_ListID);
        }
    }

    //open options menu if, mouse on GO && right click
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            optionWindow = true;
        }
    }
    #endregion
}

public class NetVarList
{
    private string ipAddress;
    private int port;
    private int listID;

    private UdpClient udpClient;
    private List<CData> cDataList = new List<CData>();
    private IPEndPoint iPEndPoint;
    private Telegram telegram;


    public NetVarList()
    {
    }

    /// <summary>
    /// create a new UDPClient. Method is not mandatory
    /// </summary>		
    public void Connect()
    {
        udpClient = new UdpClient(port);
        iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
    }

    /// <summary>
    /// closes port used by UDP-Client
    /// </summary>			
    public void Disconnect()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }

    /// <summary>
    /// Reads values from CoDeSys PLC. Datatypes are listed in "cDataList".
    /// </summary>	
    public void ReadValues()
    {
        byte[] receiveBytes = new byte[20];
        int byteCounter;
        bool waitForNextElement = true;
        int datagramCounter = 0;
        int itemCounter = 0;

        if (udpClient == null)
            this.Connect();

        // if no data is received
        if (udpClient.Available <= 20)
        {
            return;
        }

        while ((receiveBytes[8] != listID) | waitForNextElement | itemCounter != cDataList.Count)
        {
            receiveBytes = udpClient.Receive(ref iPEndPoint);

            #region createTelegramInformation
            telegram = new Telegram();
            Buffer.BlockCopy(receiveBytes, 0, telegram.Identity, 0, 4);
            telegram.ID = BitConverter.ToUInt32(receiveBytes, 4);
            telegram.Index = BitConverter.ToUInt16(receiveBytes, 8);
            telegram.SubIndex = BitConverter.ToUInt16(receiveBytes, 10);
            telegram.Items = BitConverter.ToUInt16(receiveBytes, 12);
            telegram.Length = BitConverter.ToUInt16(receiveBytes, 14);
            telegram.Counter = BitConverter.ToUInt16(receiveBytes, 16);
            telegram.Flags = receiveBytes[18];
            telegram.Checksum = receiveBytes[19];
            telegram.Data = new byte[receiveBytes.Length - 20];
            Buffer.BlockCopy(receiveBytes, 20, telegram.Data, 0, telegram.Data.Length);
            byteCounter = 20;
            #endregion

            if (telegram.Index == listID & telegram.SubIndex == datagramCounter)
            {
                // read all items from datagram
                for (int i = 0; i < telegram.Items; i++)
                {
                    CData cData = CDataList[itemCounter];
                    switch (cData.DataType)
                    {
                        case DataType.booltype:
                            cData.Value = BitConverter.ToBoolean(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 1;
                            break;
                        case DataType.bytetype:
                            cData.Value = receiveBytes[byteCounter];
                            byteCounter = byteCounter + 1;
                            break;
                        case DataType.wordtype:
                            cData.Value = BitConverter.ToUInt16(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 2;
                            break;
                        case DataType.dwordtype:
                            cData.Value = BitConverter.ToUInt32(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 4;
                            break;
                        case DataType.sinttype:
                            cData.Value = (sbyte)receiveBytes[byteCounter];
                            byteCounter = byteCounter + 1;
                            break;
                        case DataType.usinttype:
                            cData.Value = receiveBytes[byteCounter];
                            byteCounter = byteCounter + 1;
                            break;
                        case DataType.inttype:
                            cData.Value = BitConverter.ToInt16(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 2;
                            break;
                        case DataType.uinttype:
                            cData.Value = BitConverter.ToUInt16(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 2;
                            break;
                        case DataType.udinttype:
                            cData.Value = BitConverter.ToUInt32(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 4;
                            break;
                        case DataType.dinttype:
                            cData.Value = BitConverter.ToInt32(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 4;
                            break;
                        case DataType.realtype:
                            cData.Value = BitConverter.ToSingle(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 4;
                            break;
                        case DataType.lrealtype:
                            cData.Value = BitConverter.ToDouble(receiveBytes, byteCounter);
                            byteCounter = byteCounter + 8;
                            break;
                        case DataType.stringtype:
                            string stringValue = System.Text.Encoding.UTF8.GetString(receiveBytes, byteCounter, cData.FieldLength);
                            int nullSignPosition = stringValue.Length;
                            for (int j = 0; j < stringValue.Length; j++)
                            {
                                if (receiveBytes[byteCounter + j] == 0)
                                {
                                    nullSignPosition = j;
                                    break;
                                }
                            }
                            stringValue = stringValue.Substring(0, nullSignPosition);
                            cData.Value = stringValue;
                            byteCounter = byteCounter + cData.FieldLength + 1;
                            break;
                        case DataType.wstringtype:
                            string wstringValue = System.Text.Encoding.Unicode.GetString(receiveBytes, byteCounter, cData.FieldLength);
                            int wnullSignPosition = wstringValue.Length;
                            for (int j = 0; j < wstringValue.Length; j++)
                            {
                                if (receiveBytes[byteCounter + j] == 0)
                                {
                                    wnullSignPosition = j;
                                    break;
                                }
                            }
                            stringValue = wstringValue.Substring(0, wnullSignPosition);
                            cData.Value = stringValue;
                            byteCounter = byteCounter + cData.FieldLength + 1;
                            break;
                    }
                    itemCounter++;
                }
                datagramCounter++;
            }
            // if last diagram is read
            if ((CountNumberOfDatagrams(cDataList.Count) <= telegram.SubIndex) & telegram.Index == listID)
                waitForNextElement = false;
        }
    }

    private ushort sendCounter = 0;
    public void SendValues()
    {
        // create connection
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        telegram = new Telegram();
        iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        socket.EnableBroadcast = true;

        int numberOfDatagrams = CountNumberOfDatagrams(cDataList.Count)  + 1;
        int numberOfItems = cDataList.Count;
        int numberOfItemsInDatagram = 0;
        int datagramCounter = 0;
        int itemCounter = 0;
        byte[] send_buffer;

        // for every datagram
        for (int k = 0; k < numberOfDatagrams; k++)
        {
            if (sendCounter >= 65535)
                sendCounter = 0;
            else
                sendCounter++;

            numberOfItemsInDatagram = 1;
            itemCounter++;

            while ((CountNumberOfDatagrams(itemCounter) == datagramCounter) & (itemCounter != cDataList.Count))
            {
                numberOfItemsInDatagram++;
                itemCounter++;

            }
            if (CountNumberOfDatagrams(itemCounter) > datagramCounter)
            {
                itemCounter--;
                numberOfItemsInDatagram--;

            }

            #region createTelegramInformatiom
            send_buffer = new byte[20];
            UInt32[] uintarray = new UInt32[1];
            Int32[] intarray = new Int32[1];
            UInt16[] uint16array = new UInt16[1];
            Int16[] int16array = new Int16[1];
            byte[] bytearray = new byte[1];
            sbyte[] sbytearray = new sbyte[1];
            float[] floatarray = new float[1];
            double[] doublearray = new double[1];
            Buffer.BlockCopy(telegram.Identity, 0, send_buffer, 0, 4);
            uintarray[0] = telegram.ID;
            Buffer.BlockCopy(uintarray, 0, send_buffer, 4, 4);
            telegram.Index = (ushort)listID;
            uint16array[0] = telegram.Index;
            Buffer.BlockCopy(uint16array, 0, send_buffer, 8, 2);
            telegram.SubIndex = (ushort)datagramCounter;
            uint16array[0] = telegram.SubIndex;
            Buffer.BlockCopy(uint16array, 0, send_buffer, 10, 2);
            telegram.Items = (ushort)numberOfItemsInDatagram;
            uint16array[0] = telegram.Items;
            Buffer.BlockCopy(uint16array, 0, send_buffer, 12, 2);
            telegram.Counter = (ushort)sendCounter;
            uint16array[0] = telegram.Counter;
            Buffer.BlockCopy(uint16array, 0, send_buffer, 16, 2);
            send_buffer[18] = telegram.Flags;
            send_buffer[19] = telegram.Checksum;

            int byteCounter = 20;

            // for every item in datagram
            for (int i = 0; i < numberOfItemsInDatagram; i++)
            {
                switch (cDataList[i + itemCounter - numberOfItemsInDatagram].DataType)
                {
                    case DataType.booltype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 1);
                        send_buffer[byteCounter] = Convert.ToByte(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        byteCounter++;
                        break;
                    case DataType.bytetype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 1);
                        send_buffer[byteCounter] = Convert.ToByte(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        byteCounter++;
                        break;
                    case DataType.wordtype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 2);
                        uint16array[0] = Convert.ToUInt16(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(uint16array, 0, send_buffer, byteCounter, 2);
                        byteCounter = byteCounter + 2;
                        break;
                    case DataType.dwordtype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 4);
                        uintarray[0] = Convert.ToUInt32(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(uintarray, 0, send_buffer, byteCounter, 4);
                        byteCounter = byteCounter + 4;
                        break;
                    case DataType.sinttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 1);
                        sbytearray[0] = Convert.ToSByte(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(sbytearray, 0, send_buffer, byteCounter, 1);
                        byteCounter = byteCounter + 1;
                        break;
                    case DataType.usinttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 1);
                        send_buffer[byteCounter] = Convert.ToByte(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        byteCounter++;
                        break;
                    case DataType.inttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 2);
                        int16array[0] = Convert.ToInt16(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(int16array, 0, send_buffer, byteCounter, 2);
                        byteCounter = byteCounter + 2;
                        break;
                    case DataType.uinttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 2);
                        uint16array[0] = Convert.ToUInt16(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(uint16array, 0, send_buffer, byteCounter, 2);
                        byteCounter = byteCounter + 2;
                        break;
                    case DataType.dinttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 4);
                        intarray[0] = Convert.ToInt32(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(intarray, 0, send_buffer, byteCounter, 4);
                        byteCounter = byteCounter + 4;
                        break;
                    case DataType.udinttype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 4);
                        uintarray[0] = Convert.ToUInt32(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(uintarray, 0, send_buffer, byteCounter, 4);
                        byteCounter = byteCounter + 4;
                        break;
                    case DataType.realtype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 4);
                        floatarray[0] = Convert.ToSingle(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(floatarray, 0, send_buffer, byteCounter, 4);
                        byteCounter = byteCounter + 4;
                        break;
                    case DataType.lrealtype:
                        Array.Resize(ref send_buffer, send_buffer.Length + 8);
                        doublearray[0] = Convert.ToDouble(cDataList[i + itemCounter - numberOfItemsInDatagram].Value);
                        Buffer.BlockCopy(doublearray, 0, send_buffer, byteCounter, 8);
                        byteCounter = byteCounter + 8;
                        break;
                    case DataType.stringtype | DataType.wstringtype:
                        byte[] byteArray = new byte[cDataList[i + itemCounter - numberOfItemsInDatagram].FieldLength + 1];
                        char[] charArray = Convert.ToString(cDataList[i + itemCounter - numberOfItemsInDatagram].Value).ToCharArray();
                        if (charArray.Length <= cDataList[i + itemCounter - numberOfItemsInDatagram].FieldLength)
                            Array.Resize(ref charArray, (cDataList[i + itemCounter - numberOfItemsInDatagram].FieldLength + 1));
                        Array.Resize(ref send_buffer, send_buffer.Length + charArray.Length);
                        for (int j = 0; j < charArray.Length; j++)
                        {
                            byteArray[j] = (byte)charArray[j];
                        }
                        Buffer.BlockCopy(byteArray, 0, send_buffer, byteCounter, byteArray.Length);
                        byteCounter = byteCounter + charArray.Length;
                        break;
                    default:
                        break;
                }
            }
            telegram.Length = (ushort)send_buffer.Length;
            uint16array[0] = telegram.Length;
            Buffer.BlockCopy(uint16array, 0, send_buffer, 14, 2);
            #endregion

            socket.SendTo(send_buffer, iPEndPoint);
            datagramCounter++;
        }
    }

    /// <summary>
    /// Calculate number of datagrams necessary for the number of items which are defined by "items". Datagrams will be 
    /// devided if datagram size is more than 256 byte
    /// </summary>
    /// <param name="items">Item number for calculation of datagrams</param>    
    /// <returns>Number of datagrams necessary</returns>   
    private int CountNumberOfDatagrams(int items)
    {
        int countValue = 0;
        int valueBefore = 0;
        int valueAfter = 0;
        int returnValue;
        for (int i = 0; (i < cDataList.Count & i < items); i++)
        {
            valueBefore = countValue;
            //1 Byte width datatype
            if ((cDataList[i].DataType == DataType.booltype)
                | (cDataList[i].DataType == DataType.bytetype)
                | (cDataList[i].DataType == DataType.sinttype)
                | (cDataList[i].DataType == DataType.usinttype))
            {
                countValue = countValue + 1;
                valueAfter = countValue;


            }
            //2 Byte width datatype
            if ((cDataList[i].DataType == DataType.wordtype)
                | (cDataList[i].DataType == DataType.inttype)
                | (cDataList[i].DataType == DataType.uinttype))
            {
                countValue = countValue + 2;
                valueAfter = countValue;
                if (valueAfter % 256 <= valueBefore % 256 & valueAfter % 256 != 0)
                {
                    countValue = (int)(256 * Math.Ceiling((float)valueBefore / 256)) + 2;
                }
            }
            //4 Byte width datatype
            if ((cDataList[i].DataType == DataType.dwordtype)
                | (cDataList[i].DataType == DataType.udinttype)
                | (cDataList[i].DataType == DataType.dinttype)
                | (cDataList[i].DataType == DataType.realtype))
            {
                countValue = countValue + 4;
                valueAfter = countValue;
                if (valueAfter % 256 <= valueBefore % 256 & valueAfter % 256 != 0)
                {
                    countValue = (int)(256 * Math.Ceiling((float)valueBefore / 256)) + 4;
                }
            }
            //8 Byte width datatype
            if (cDataList[i].DataType == DataType.lrealtype)
            {
                countValue = countValue + 8;
                valueAfter = countValue;
                if (valueAfter % 256 <= valueBefore % 256 & valueAfter % 256 != 0)
                {
                    countValue = (int)(256 * Math.Ceiling((float)valueBefore / 256)) + 8;
                }
            }
            //String datatype
            if (cDataList[i].DataType == DataType.stringtype
                | cDataList[i].DataType == DataType.wstringtype)
            {
                countValue = countValue + cDataList[i].FieldLength + 1;
                valueAfter = countValue;
                if (valueAfter % 256 <= valueBefore % 256 & valueAfter % 256 != 0)
                {
                    countValue = (int)(256 * Math.Ceiling((float)valueBefore / 256)) + cDataList[i].FieldLength + 1;
                }
            }
        }

        returnValue = (int)(Math.Floor((float)countValue / 256));
        if ((countValue % 256) == 0 & returnValue > 0)
            returnValue--;
        return returnValue;
    }

    public void ImportGVLFile(string filename)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        cDataList.Clear();
        XmlReader reader = XmlReader.Create(filename);

        reader.ReadToFollowing("Declarations");
        if (reader.NodeType == XmlNodeType.CDATA)
        {
            string cdata = reader.ReadString();
            cdata = cdata.Replace(" ", "");
            cdata = cdata.Replace("=", "");
            cdata = cdata.Replace("'", "");
            cdata = cdata.Replace("\u000A", "");
            cdata = cdata.Replace("\u0009", "");
            cdata = cdata.Replace("VAR_GLOBAL", "");
            cdata = cdata.Replace("END_VAR", "");
            cdata = cdata.Replace("{attributequalified_only}", "");
            string[] splitdata = cdata.Split(new char[] { ';' });
            foreach (string data in splitdata)
            {
                if (data != "")
                {
                    string[] attributes = new string[3];
                    attributes = data.Split(new char[] { ':' });
                    CData item = new CData();
                    item.VariableName = attributes[0];
                    switch (attributes[1])
                    {
                        case "BOOL":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.booltype);
                            else
                                item = new CData(attributes[0], DataType.booltype, bool.Parse(attributes[2]));
                            break;
                        case "BYTE":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.bytetype);
                            else
                                item = new CData(attributes[0], DataType.bytetype, byte.Parse(attributes[2]));
                            break;
                        case "WORD":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.wordtype);
                            else
                                item = new CData(attributes[0], DataType.wordtype, UInt16.Parse(attributes[2]));
                            break;
                        case "DWORD":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.dwordtype);
                            else
                                item = new CData(attributes[0], DataType.dwordtype, UInt32.Parse(attributes[2]));
                            break;
                        case "SINT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.sinttype);
                            else
                                item = new CData(attributes[0], DataType.sinttype, sbyte.Parse(attributes[2]));
                            break;
                        case "USINT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.usinttype);
                            else
                                item = new CData(attributes[0], DataType.usinttype, byte.Parse(attributes[2]));
                            break;
                        case "INT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.inttype);
                            else
                                item = new CData(attributes[0], DataType.inttype, Int16.Parse(attributes[2]));
                            break;
                        case "UINT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.uinttype);
                            else
                                item = new CData(attributes[0], DataType.uinttype, UInt16.Parse(attributes[2]));
                            break;
                        case "DINT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.dinttype);
                            else
                                item = new CData(attributes[0], DataType.dinttype, Int32.Parse(attributes[2]));
                            break;
                        case "UDINT":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.udinttype);
                            else
                                item = new CData(attributes[0], DataType.udinttype, UInt32.Parse(attributes[2]));
                            break;
                        case "REAL":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.realtype);
                            else
                                item = new CData(attributes[0], DataType.realtype, float.Parse(attributes[2]));
                            break;
                        case "LREAL":
                            if (attributes.Length < 3)
                                item = new CData(attributes[0], DataType.lrealtype);
                            else
                                item = new CData(attributes[0], DataType.lrealtype, double.Parse(attributes[2]));
                            break;
                        default:
                            break;
                    }
                    if (attributes[1].StartsWith("STRING"))
                    {
                        item.DataType = DataType.stringtype;
                        attributes[1] = attributes[1].Replace("STRING(", "");
                        attributes[1] = attributes[1].Replace(")", "");
                        item.FieldLength = int.Parse(attributes[1]);
                        item.Value = attributes[2];
                    }
                    if (attributes[1].StartsWith("WSTRING"))
                    {
                        item.DataType = DataType.stringtype;
                        attributes[1] = attributes[1].Replace("WSTRING(", "");
                        attributes[1] = attributes[1].Replace(")", "");
                        item.FieldLength = int.Parse(attributes[1]);
                        item.Value = attributes[2];
                    }
                    cDataList.Add(item);
                }
            }
        }
        reader.ReadToFollowing("ListIdentifier");
        listID = int.Parse(reader.ReadInnerXml());
        reader.ReadToFollowing("ProtocolSetting");
        Console.Write(reader.HasAttributes);
        ipAddress = reader["Value"];
        reader.ReadToNextSibling("ProtocolSetting");
        port = int.Parse(reader["Value"]);

        reader.Close();
    }

    public void ExportGVLFile(string filename)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode xmlRoot, xmlNode, xmlNode2, xmlNode3;
        XmlAttribute xmlAttribute;
        xmlRoot = xmlDocument.CreateElement("GVL");
        xmlDocument.AppendChild(xmlRoot);

        xmlNode = xmlDocument.CreateElement("Declarations");
        #region CreateCDataSection
        string cDataSection = '\u000A' + "VAR_GLOBAL";
        for (int i = 0; i < cDataList.Count; i++)
        {
            if (cDataList[i].VariableName != null)
                cDataSection = cDataSection + '\u000A' + '\u0009' + cDataList[i].VariableName;
            else
                cDataSection = cDataSection + '\u000A' + '\u0009' + "variable" + i.ToString();

            switch (cDataList[i].DataType)
            {
                case DataType.booltype:
                    cDataSection = cDataSection + " : BOOL";
                    break;
                case DataType.bytetype:
                    cDataSection = cDataSection + " : BYTE";
                    break;
                case DataType.wordtype:
                    cDataSection = cDataSection + " : WORD";
                    break;
                case DataType.dwordtype:
                    cDataSection = cDataSection + " : DWORD";
                    break;
                case DataType.sinttype:
                    cDataSection = cDataSection + " : SINT";
                    break;
                case DataType.usinttype:
                    cDataSection = cDataSection + " : USINT";
                    break;
                case DataType.inttype:
                    cDataSection = cDataSection + " : INT";
                    break;
                case DataType.uinttype:
                    cDataSection = cDataSection + " : UINT";
                    break;
                case DataType.dinttype:
                    cDataSection = cDataSection + " : DINT";
                    break;
                case DataType.udinttype:
                    cDataSection = cDataSection + " : UDINT";
                    break;
                case DataType.realtype:
                    cDataSection = cDataSection + " : REAL";
                    break;
                case DataType.lrealtype:
                    cDataSection = cDataSection + " : LREAL";
                    break;
                case DataType.stringtype:
                    cDataSection = cDataSection + " : STRING(" + cDataList[i].FieldLength + ")";
                    break;
                case DataType.wstringtype:
                    cDataSection = cDataSection + " : WSTRING(" + cDataList[i].FieldLength + ")";
                    break;
            }

            if (cDataList[i].Value != null)
            {
                if (cDataList[i].DataType == DataType.stringtype
                    | cDataList[i].DataType == DataType.wstringtype)
                    cDataSection = cDataSection + " := '" + cDataList[i].Value + "';";
                else
                    cDataSection = cDataSection + " := " + cDataList[i].Value.ToString() + ";";
            }
            else
                cDataSection = cDataSection + ";";

        }
        cDataSection = cDataSection + '\u000A' + "END_VAR";
        #endregion

        xmlNode2 = xmlDocument.CreateCDataSection(cDataSection);
        xmlNode.AppendChild(xmlNode2);
        xmlRoot.AppendChild(xmlNode);

        xmlNode = xmlDocument.CreateElement("NetvarSettings");
        xmlAttribute = xmlDocument.CreateAttribute("Protocol");
        xmlAttribute.Value = "UDP";
        xmlNode.Attributes.Append(xmlAttribute);
        xmlNode2 = xmlDocument.CreateElement("ListIdentifier");
        xmlNode2.InnerText = listID.ToString();
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("Pack");
        xmlNode2.InnerText = "True";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("Checksum");
        xmlNode2.InnerText = "False";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("Acknowledge");
        xmlNode2.InnerText = "False";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("CyclicTransmission");
        xmlNode2.InnerText = "True";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("TransmissionOnChange");
        xmlNode2.InnerText = "False";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("TransmissionOnEvent");
        xmlNode2.InnerText = "False";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("Interval");
        xmlNode2.InnerText = "T#20ms";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("MinGap");
        xmlNode2.InnerText = "T#20ms";
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("EventVariable");
        xmlNode.AppendChild(xmlNode2);
        xmlNode2 = xmlDocument.CreateElement("ProtocolSettings");
        xmlNode3 = xmlDocument.CreateElement("ProtocolSetting");
        xmlAttribute = xmlDocument.CreateAttribute("Name");
        xmlAttribute.Value = "Broadcast Adr.";
        xmlNode3.Attributes.Append(xmlAttribute);
        xmlAttribute = xmlDocument.CreateAttribute("Value");
        xmlAttribute.Value = ipAddress;
        xmlNode3.Attributes.Append(xmlAttribute);
        xmlNode2.AppendChild(xmlNode3);
        xmlNode3 = xmlDocument.CreateElement("ProtocolSetting");
        xmlAttribute = xmlDocument.CreateAttribute("Name");
        xmlAttribute.Value = "Port";
        xmlNode3.Attributes.Append(xmlAttribute);
        xmlAttribute = xmlDocument.CreateAttribute("Value");
        xmlAttribute.Value = port.ToString();
        xmlNode3.Attributes.Append(xmlAttribute);
        xmlNode2.AppendChild(xmlNode3);
        xmlNode.AppendChild(xmlNode2);
        xmlRoot.AppendChild(xmlNode);
        xmlDocument.Save(filename + ".GVL");
    }

    /// <summary>
    /// Network identifier of CoDeSys Network Variablelist
    /// </summary>
    public int ListID
    {
        get
        {
            return listID;
        }
        set
        {
            listID = value;
        }
    }

    /// <summary>
    /// Port for Dataexchange
    /// </summary>
    public int Port
    {
        get
        {
            return port;
        }
        set
        {
            port = value;
        }
    }

    /// <summary>
    /// IP-Adress for Send-Operation
    /// </summary>
    public string IPAdress
    {
        get
        {
            return ipAddress;
        }
        set
        {
            ipAddress = value;
        }
    }

    /// <summary>
    /// Listed Datatypes which corresponds to the Network variableList
    /// </summary>
    public List<CData> CDataList
    {
        get
        {
            return cDataList;
        }
        set
        {
            cDataList = value;
        }
    }

    /// <summary>
    /// Telegram information
    /// </summary>
    public Telegram Telegram
    {
        get
        {
            return telegram;
        }
    }
}

public enum DataType
{
    booltype,
    bytetype,
    wordtype,
    dwordtype,
    sinttype,
    usinttype,
    inttype,
    uinttype,
    dinttype,
    udinttype,
    realtype,
    lrealtype,
    stringtype,
    wstringtype
}

/// <summary>
/// Datatype information consisting of Datatype and fieldlength (only necessary for Strings)
/// </summary>
public class CData
{
    private string variableName;
    private int fieldLength = 80;
    private DataType dataType;
    private object value;

    public CData()
    {
    }

    public CData(string variableName, DataType dataType)
    {
        this.variableName = variableName;
        this.dataType = dataType;
    }

    public CData(string variableName, int fieldLength, DataType dataType)
    {
        this.variableName = variableName;
        this.fieldLength = fieldLength;
        this.dataType = dataType;
    }

    public CData(string variableName, DataType dataType, object value)
    {
        this.variableName = variableName;
        this.dataType = dataType;
        this.value = value;
    }

    public CData(string variableName, int fieldLength, DataType dataType, object value)
    {
        this.variableName = variableName;
        this.fieldLength = fieldLength;
        this.dataType = dataType;
        this.value = value;
    }

    public DataType DataType
    {
        get
        {
            return dataType;
        }
        set
        {
            dataType = value;
        }
    }

    public int FieldLength
    {
        get
        {
            return fieldLength;
        }
        set
        {
            fieldLength = value;
        }
    }

    public object Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
        }
    }

    public string VariableName
    {
        get
        {
            return variableName;
        }
        set
        {
            variableName = value;
        }
    }

}

public class Telegram
{
    /// <summary>
    /// CoDeSys protocol identity code "3S-0" (Byte0="0"; Byte1="-"; Byte2="S"; Byte3="3")
    /// </summary>
    public byte[] Identity = new byte[4] { 0, 45, 83, 51 };

    /// <summary>
    /// ID for Network Variables "0"
    /// </summary>
    public UInt32 ID = 0;

    /// <summary>
    /// Cob-ID
    /// </summary>
    public UInt16 Index = 1;

    /// <summary>
    /// If "Pack-Variables" is disabled - Message ID
    /// </summary>
    public UInt16 SubIndex;

    /// <summary>
    /// Number of Variables
    /// </summary>
    public UInt16 Items;

    /// <summary>
    /// Total Size of the Message incl. Header
    /// </summary>
    public UInt16 Length;

    /// <summary>
    /// Counts the number of sent telegrams
    /// </summary>
    public UInt16 Counter;

    /// <summary>
    /// Bit0: Send-acknowledgement desired; Bit1: Check of checksum desired; Bit2: Invalid checksum
    /// </summary>
    public byte Flags;

    /// <summary>
    /// Checksum of the datagram
    /// </summary>
    public byte Checksum;

    /// <summary>
    /// Data
    /// </summary>
    public byte[] Data;
}
