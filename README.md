# InventorExport

GCR student of VolgSTU IVT- 465 Kirill Igorevich Anshakov.

This program is an Add-in for Autodesk Inventor 2019. This work is written in C#. The program is designed for easy export of assembly models in OBJ format with preservation of the hierarchy of connections.

---------------------------------------------------------------------------------------------------------------------------------------------------

💿 The program requires installation of Autodesk Inventor 2019, as well as Visual Studio Community 2019 and tools for the development of c# applications. You also need the .Net framework 4.7.2

💿 To install the Add-in, you need to move the "ExportAddin" folder to the following path "C:\ProgramData\Autodesk\Inventor 2019\Addins" then in the "tools" panel go to the Add-ins folder and enable as shown in the figure 

![tools](https://github.com/KirillRustyNail/InventorExport/blob/main/images/tools.png)

After a successful connection a new "ExporforVR" panel will appear , which contains the necessary buttons to connect to the database and export.

![tools](https://github.com/KirillRustyNail/InventorExport/blob/main/images/Addin%20Tab.png)


---------------------------------------------------------------------------------------------------------------------------------------------------

💿 To assemble the project you need the following 

1) In the project build settings, specify the output path as shown in the figure 

![tools](https://github.com/KirillRustyNail/InventorExport/blob/main/images/Output%20data.png)

2) In the debugging tab, specify "Run external program" as follows "\Autodesk\Inventor 2019\Bin\Inventor.exe".

![tools](https://github.com/KirillRustyNail/InventorExport/blob/main/images/Setup.png)
