using System;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Management;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace WMIScanner
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    
    public partial class MainForm : Form
    {
    
    static String name="Name";
    static String version="Version";
    static String serialnumber="SerialNumber";
    ManagementObjectSearcher  mgmtSrchr;
    ManagementObjectSearcher  mgmtSrchr1;
    ManagementObjectSearcher  mgmtSrchr2;
     ManagementObjectSearcher  mgmtSrchr3;
     ManagementScope mgmtScope;

    SelectQuery osQuery;
    SelectQuery osQuery1;
     SelectQuery osQuery2;
    SelectQuery osQuery3;
    
    Thread main,thread1,thread2,thread3,thread4,thread5,thread6,thread7,thread8,thread9,thread10;

        static int n=0;
         static int v=0;
         static int sn=0;
 static String Query;
 static String adress;
 static String subnetn;

 static int sub;
 static int kon;
 static int comn;
 static int comv;
 static int comsn;
 static int rozm,rozm1;
 static int q,b,c;//Lp.
 static String []porz;
 string []mask;
Object []ind=new object[sub];


 public void savequery_to_file(){
     
         DialogResult dialogButton=MessageBox.Show("Do you want save this query?","Save query",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
         if(dialogButton==DialogResult.Yes){
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("query.txt", true))
            {
                file.WriteLine(query.Text);
            }  
         }
 }
   
 public void readquery_from_file(){
     try
        {   
            using (StreamReader sr = new StreamReader("query.txt"))
            {
                String line = sr.ReadToEnd();
                rich.Text=line.ToString();
            }
        }
        catch 
        {
           MessageBox.Show("System didn't find the file 'query.txt' in this location","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }

         public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //

            InitializeComponent();
            readquery_from_file();
    
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
            
         
         public void scan(){        

Ping ping;
PingReply reply;
              // this function scanning computers
              dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Clear();}));
                    this.timer.Stop();
                    progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(-100);}));
            n=0;
             v=0;
             sn=0;
             String IPa=Convert.ToString(ip1.Text);
             
                this.timer.Start();        //timer run
                
string NazwaHosta = Dns.GetHostName();
IPHostEntry AdresyIP = Dns.GetHostEntry(NazwaHosta);
Query=Convert.ToString(query.Text);
mask=IPa.Split('/');


//IP-------------------------------------------------------------------
if(mask.Length==1){
    Wyszukaj.Invoke(new System.Action(delegate(){Wyszukaj.Enabled=false;}));
    stop.Invoke(new System.Action(delegate(){stop.Enabled=false;}));
    //filtr.Invoke(new System.Action(delegate(){filtr.Enabled=false;}));
    try{
ping=new Ping();
        reply=ping.Send(IPa,900);
    }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,IPa,"not ping");}));}
    ping=new Ping();    
    reply=ping.Send(IPa,900);
        if(reply.Status==IPStatus.Success)    {
            try{
 mgmtScope = new ManagementScope("\\\\"+IPa+"\\root\\cimv2");//
mgmtScope.Connect();
        }catch{
        dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,IPa,"you do not have access to this device",null,null,null,null);}));}
        
        try{
 osQuery = new SelectQuery(Query);
 mgmtSrchr = new ManagementObjectSearcher(mgmtScope, osQuery);
            
string [] a=Query.Split(' ',',');    //save every words of query on elements of matrix
for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
     osQuery1 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr1 = new ManagementObjectSearcher(mgmtScope, osQuery1);
 
foreach (var result in mgmtSrchr.Get())
{
    foreach (var result1 in mgmtSrchr1.Get()){
    
        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),result["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result["Version"].ToString(),null);}));}
        //SerialNumber
         if(sn==3&&v==0&& n==0){   
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),result["Name"].ToString(),result["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),result["Name"].ToString(),null,result["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result["Version"].ToString(),result["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,result1["Name"].ToString(),null,result1["UserName"].ToString(),result["Name"].ToString(),result["Version"].ToString(),result["SerialNumber"].ToString());}));
            }
        }
}

        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,IPa,"query don't work");}));}

    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(null,IPa,"not ping");})); }
    
    progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(100);}));

      MessageBox.Show("Scanning is finished","Koniec",MessageBoxButtons.OK,MessageBoxIcon.Information);
    
      Wyszukaj.Invoke(new System.Action(delegate(){Wyszukaj.Enabled=true;}));
    stop.Invoke(new System.Action(delegate(){stop.Enabled=true;}));

}
        
//subnet---------------------------------------------------------------

if(mask.Length==2){
    
          Wyszukaj.Invoke(new System.Action(delegate(){Wyszukaj.Enabled=false;}));
              //filtr.Invoke(new System.Action(delegate(){filtr.Enabled=true;}));
     subnetn=mask[0].TrimEnd( new Char[] {'0'});
     string[]adr=subnetn.Split('.');
     rozm1=adr.Length;
      ConnectionOptions conOp = new ConnectionOptions(); 
      conOp.Authentication = AuthenticationLevel.PacketPrivacy;
kon=int.Parse(mask[1]);
if(kon==24){
    sub=255;
      //Wyszukaj.Invoke(new System.Action(delegate(){Wyszukaj.Enabled=false;}));

thread1=new Thread(()=>
{
         for(int i=1;i<26;i++){

        adress=subnetn+i.ToString();
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
    
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  

            
}

  });
//------------------------------------------------------------------------------------------------------
thread2=new Thread(()=>{{
                           for(int i=25;i<50;i++){
         
        adress=subnetn+i.ToString();
    
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
         
    dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);})); 
                                   
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
      }});
//----------------------------------------------------------------------------------------------------------------------------
thread3=new Thread(()=>{{
          for(int i = 50;i<75;i++){
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
                  dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));
     
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
//thread3.Abort();
 }});
//-----------------------------------------------------------------------------------------------------------------------
thread4=new Thread(()=>{{
        for(int i = 75;i<100;i++){
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        
        
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
     dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));
                               
                           
    
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

      }});
//--------------------------------------------------------------------------------------------------------------------------
thread5=new Thread(()=>{{
                       for(int i = 100;i<125;i++){
          
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
         
         dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));

         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
 //thread5.Abort();
 }});
 
 //---------------------------------------------------------------------------------------------------
 thread6=new Thread(()=>{{
                       for(int i = 125;i<151;i++){
          
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
      
        
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
              dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));       
         
        
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

 }});

//------------------------------------------------------------------------------------------------------------------
thread7=new Thread(()=>{{
                       for(int i = 151;i<177;i++){
          
            adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
              
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
        dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);})); 
        

         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}
 //thread7.Abort();
 }});
 
 //-------------------------------------------------------------------------------------------------------
 thread8=new Thread(()=>{{
                       for(int i = 177;i<203;i++){
          
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
    
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
                    dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));

             
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}            //progressbar run    
}  
      
}
 }});
 
 //----------------------------------------------------------------------------------------------------------
 thread9=new Thread(()=>{{
                       for(int i = 203;i<231;i++){
          
            adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
            
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}

            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
    
     dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==512){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1024){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                            }
                            

 }});
 //----------------------------------------------------------------------------------------------------------
 thread10=new Thread(()=>{{
                       for(int i = 201;i<255;i++){
          
                adress=subnetn+i.ToString();
        
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
              
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
    
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
         
             dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[3],ListSortDirection.Ascending);}));  
        
     
         if(sub==255){
             if(i%5==0&&i<230){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}            //progressbar run    
if(i%5==0&&i>=230){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}    
     }

           
}

 }});
}

if(kon==23){
    sub=511;
    
    thread1=new Thread(()=>
{
         for(int i=1;i<51;i++){

        adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();

              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}

  });
//------------------------------------------------------------------------------------------------------
thread2=new Thread(()=>{{
                           for(int i=51;i<101;i++){
         
            adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
      }});
//----------------------------------------------------------------------------------------------------------------------------
thread3=new Thread(()=>{{
          for(int i = 101;i<151;i++){
                    adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
//thread3.Abort();
 }});
//-----------------------------------------------------------------------------------------------------------------------
thread4=new Thread(()=>{{
        for(int i =151;i<201;i++){
                adress=subnetn+i.ToString();
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

      }});
//--------------------------------------------------------------------------------------------------------------------------
thread5=new Thread(()=>{{
                       for(int i = 201;i<255;i++){
          
                    adress=subnetn+i.ToString();
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
 //thread5.Abort();
 }});
 
 //---------------------------------------------------------------------------------------------------
thread6=new Thread(()=>{{
         for(int i = 1;i<51;i++){
                                adress=adr[0]+"."+adr[1]+"."+c.ToString()+"."+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
porz=adress.Split('.');
        rozm=porz.Length;
    q=int.Parse(porz[rozm-1]);
          try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

 }});

//------------------------------------------------------------------------------------------------------------------
thread7=new Thread(()=>{{
                       for(int i = 51;i<101;i++){
adress=adr[0]+"."+adr[1]+"."+c.ToString()+"."+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
              porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}
 //thread7.Abort();
 }});
 
 //-------------------------------------------------------------------------------------------------------
 thread8=new Thread(()=>{{
                       for(int i = 101;i<151;i++){
              adress=adr[0]+"."+adr[1]+"."+c.ToString()+"."+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}
 }});
 
 //----------------------------------------------------------------------------------------------------------
 thread9=new Thread(()=>{{
                       for(int i = 151;i<201;i++){
           adress=adr[0]+"."+adr[1]+"."+c.ToString()+"."+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                            }

 }});
 //-----------------------------------------------------------------------------------------
 thread10=new Thread(()=>{{
                       for(int i = 201;i<255;i++){
          
                    adress=adr[0]+"."+adr[1]+"."+c.ToString()+"."+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}

 }});
}


if(kon>=25){
    sub=128;
    
    thread1=new Thread(()=>
{
         for(int i=1;i<12;i++){

        adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();

              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}

  });
//------------------------------------------------------------------------------------------------------
thread2=new Thread(()=>{{
                           for(int i=13;i<25;i++){
         
            adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
      }});
//----------------------------------------------------------------------------------------------------------------------------
thread3=new Thread(()=>{{
          for(int i = 26;i<38;i++){
                    adress=subnetn+i.ToString();

        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
  
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
//thread3.Abort();
 }});
//-----------------------------------------------------------------------------------------------------------------------
thread4=new Thread(()=>{{
        for(int i =39;i<51;i++){
                adress=subnetn+i.ToString();
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

      }});
//--------------------------------------------------------------------------------------------------------------------------
thread5=new Thread(()=>{{
                       for(int i = 52;i<64;i++){
          
                    adress=subnetn+i.ToString();
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}
 //thread5.Abort();
 }});
 
 //---------------------------------------------------------------------------------------------------
thread6=new Thread(()=>{{
         for(int i = 65;i<72;i++){
                                 adress=subnetn+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
porz=adress.Split('.');
        rozm=porz.Length;
    q=int.Parse(porz[rozm-1]);
          try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);


string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);

      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
 
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}

}

 }});

//------------------------------------------------------------------------------------------------------------------
thread7=new Thread(()=>{{
                       for(int i = 73;i<85;i++){
 adress=subnetn+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
              porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}

 }});
 
 //-------------------------------------------------------------------------------------------------------
 thread8=new Thread(()=>{{
                       for(int i = 85;i<97;i++){
             adress=subnetn+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}
 }});
 
 //----------------------------------------------------------------------------------------------------------
 thread9=new Thread(()=>{{
                       for(int i = 98;i<112;i++){
         adress=subnetn+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);
        
              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                            }

 }});
 //-----------------------------------------------------------------------------------------
 thread10=new Thread(()=>{{
                       for(int i = 113;i<127;i++){
          
               adress=subnetn+i.ToString();
        
        b=int.Parse(adr[2]);
        c=b+1;
        porz=adress.Split('.');
        rozm=porz.Length;
        q=int.Parse(porz[rozm-1]);

              try{
         ping=new Ping();
         reply=ping.Send(adress,900);
        }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping",null,null,null,null);}));}
         ping=new Ping();
         reply=ping.Send(adress,900);
          if(reply.Status==IPStatus.Success)    {

            try{
             mgmtScope = new ManagementScope("\\\\"+adress+"\\root\\cimv2");
             mgmtScope.Connect();
              }catch{
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"you do not have access to this device",null,null,null,null);}));}
            
                try{
 osQuery2 = new SelectQuery(Query);
 mgmtSrchr2 = new ManagementObjectSearcher(mgmtScope, osQuery2);

string[] a=Query.Split(' ',',');    //save every words of query on elements of matrix

for(int j=0;j<4;j++){
    comn=String.Compare(a[j],name,true);
    comv=String.Compare(a[j],version,true);
    comsn=String.Compare(a[j],serialnumber,true);

     if(comn==0){
            n=1;
            
        }
        if(comv==0){
            v=2;
        }
     if(comsn==0){
            sn=3;
        }
}

 //search username and computername 
 osQuery3 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");
 mgmtSrchr3 = new ManagementObjectSearcher(mgmtScope, osQuery3);
      
 foreach (var result2 in mgmtSrchr2.Get())
{
    foreach (var result1 in mgmtSrchr3.Get()){

        //Name
        if(n==1&&v==0&& sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,null);}));
        }
        //Version
         if(v==2&&n==0&& sn==0){
 dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),null);}));}
        
        //SerialNumber
         if(sn==3&&v==0&& n==0){  
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,null,result2["SerialNumber"].ToString());}));}
        //Name and Version        
         if(n==1&&v==2&&sn==0){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),null);}));
}
        // Name and SerialNumber
         if(n==1&&v==0&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),null,result2["SerialNumber"].ToString());}));
}
        //Version and SerialNumber
         if(n==0&&v==2&&sn==3){
            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),null,result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
}
        //All        
        if(n==1&&v==2&&sn==3){

            dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,result1["Name"].ToString(),null,result1["UserName"].ToString(),result2["Name"].ToString(),result2["Version"].ToString(),result2["SerialNumber"].ToString());}));
        }
        }
}
            }catch{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q,adress,"query don't work");}));}
        
    }else{dataGridView.Invoke(new System.Action(delegate(){dataGridView.Rows.Add(q+254,adress,"not ping");}));}
               
                             
dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
         
         
         if(sub==255){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(2);}));}            //progressbar run    
}  
      
           if(sub==511){
             if(i%5==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
                    if(sub==1023){
             if(i%10==0){
                 progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(1);}));}
}
}

 }});
}

               thread1.Start();
               thread2.Start();
             thread3.Start();
             thread4.Start();
             thread5.Start();   
            thread6.Start();
             thread7.Start();
             thread8.Start();
             thread9.Start();
             thread10.Start(); 
     
}

         }
         
         //after click Search button
         void Button1Click(object sender, EventArgs e)
            {//Wielowątkowość
                 this.timer.Stop();
                    progressBar.Invoke(new System.Action(delegate(){progressBar.Increment(-100);}));
                        this.timer.Start();
                        if(ip1.Text.Equals("")||query.Text.Equals("")){
                        	 MessageBox.Show("You didn't type IP address or query","Empty area",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                        }else{
    main=new Thread(new ThreadStart(scan));
    main.Start();
                        }
         }
        //after choose 'save result' option
        void saver(object sender, EventArgs e)
        {      
saveFileDialog1.InitialDirectory="C:/Users/";
    saveFileDialog1.Title="Save as Excel File";
saveFileDialog1.FileName="";
saveFileDialog1.Filter="Excel|*.xlsx";

if(saveFileDialog1.ShowDialog()!=DialogResult.Cancel){
       _Application excel = new Microsoft.Office.Interop.Excel.Application(); 
       _Workbook workbook = excel.Workbooks.Add(Type.Missing); 
       _Worksheet worksheet = (Worksheet)excel.ActiveSheet;

            for(int i=1;i<dataGridView.Columns.Count+1;i++){
                excel.Cells[1,i]=dataGridView.Columns[i-1].HeaderText;
            }
            for(int i=0;i<dataGridView.Rows.Count;i++){
            for(int j=0;j<dataGridView.Columns.Count; j++){
                excel.Cells[i+2,j+1]=dataGridView.Rows[i].Cells[j].Value;
            }
}
            excel.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
            excel.ActiveWorkbook.Saved=true;
            excel.Quit();
        }
}
        
        //after choose 'save query' option
        void saveq(object sender, EventArgs e)
        {
            savequery_to_file();
            readquery_from_file();
        }
        
    void stopp(object sender, EventArgs e)
        {
            thread1.Abort();
             thread2.Abort();
             thread3.Abort();
             thread4.Abort();
             thread5.Abort();   
            thread6.Abort();
             thread7.Abort();
             thread8.Abort();
             thread9.Abort();
             thread10.Abort();  

                          Wyszukaj.Invoke(new System.Action(delegate(){Wyszukaj.Enabled=true;}));
                          
             dataGridView.Invoke(new System.Action(delegate(){dataGridView.Sort(dataGridView.Columns[0],ListSortDirection.Ascending);}));
        MessageBox.Show("You stop a scan","Stop",MessageBoxButtons.OK,MessageBoxIcon.Information);

    }
    
        void Close(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
     /*   void filter(object sender, EventArgs e)
        {   
            
            //try{
    for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
        dgv1.Text=dataGridView.Rows[i].Cells[1].Value.ToString();
        dgv2.Text=dataGridView.Rows[i].Cells[2].Value.ToString();
        dgv3.Text=dataGridView.Rows[i].Cells[3].Value.ToString();
        dgv4.Text=dataGridView.Rows[i].Cells[4].Value.ToString();        
        dgv5.Text=dataGridView.Rows[i].Cells[5].Value.ToString();
        dgv6.Text=dataGridView.Rows[i].Cells[6].Value.ToString();
        
        for(int j=1;j<dataGridView.Rows.Count-1;j++){
            dgv21.Text=dataGridView.Rows[i].Cells[1].Value.ToString();
        dgv22.Text=dataGridView.Rows[i].Cells[2].Value.ToString();
        dgv23.Text=dataGridView.Rows[i].Cells[3].Value.ToString();
        dgv24.Text=dataGridView.Rows[i].Cells[4].Value.ToString();        
        dgv25.Text=dataGridView.Rows[i].Cells[5].Value.ToString();
        dgv26.Text=dataGridView.Rows[i].Cells[6].Value.ToString();
        }
        if(dgv1.Text==dgv21.Text&&dgv2==dgv22&&dgv3==dgv23&&dgv4==dgv24&&dgv5==dgv25&&dgv6==dgv26)
        {
            dataGridView.Rows.RemoveAt(i);
        }
    }
             
    
    }*/

}
}