# WMIScanner
 WMIScanner is a program for collecting information such as name, version and serial number given software or component. 
 Computer scanning takes place over the network using the tool built into Windows- WMI.We give the IP address 
 or subnet mask (input format with using CIDR, currently the application is prepared on the subnet: / 23, / 24, / 25 and those with 
 fewer addresses, but you have to stop the application yourself after the appropriate number of scanned computers). 
 Queries should be written using WQL, a few examples are saved in the query.txt file, which must be in the same folder 
 as the WMIScanner.exe file.This queries are displayed in the field on the right, we can also save new ones.
 Similarly, we could do with results, which are saved to an Excel file.
 
 If you want to use WMIScanner, download WMIScanner.exe from bin/Debug catalog 
