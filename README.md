# ToolSet
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>Set project version</h3>
<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>Writing this in the command line will search the working directory [working directory] for the [project name].csproj replace the version number using [version]</p>
<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>You can use %cd% for the working directory if the project you mean to change is in the same folder you are running the command from</p>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">toolset -p [project name] &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;[version] -d [working directory]</p>
</div>
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>&nbsp;</h3>
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>Upload NuGet</h3>
<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>This will search for any .nuget file in the [working directory] and upload it to the location specified in the [NuGet server] location</p>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">toolset -n [NuGet server] -d [working directory]</p>
</div>
<ul style="list-style-type: disc;">
    <li>The NuGet server location can either be a local path or an FTP path</li>
    <li>To upload using FTP you can write the location using like this</li>
</ul>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">ftp:[ftp user]/[ftp password]@[server name]:[ftp port]::[A:active,P:passive]::[target folder in ftp server]</p>
</div>
<div style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>
    <ul style="margin-bottom:0in;list-style-type: disc;">
        <li style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>You can use <span style="color:#C45911;font-weight:bold;"><span style="line-height:107%;font-size:13px;">%cd%</span></span> for the working directory if the project you mean to change is in the same folder you are running the command from</li>
    </ul>
</div>
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>Compress Folder</h3>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">toolset -z &ldquo;[folder to be compressed]&rdquo;&nbsp; &nbsp; &nbsp;&nbsp;&ldquo;[target zip file path]&rdquo;</p>
</div>
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>Upload or download</h3>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">toolset &ndash;c &quot;[file path]&quot; &ldquo;[folder path]&rdquo;</p>
</div>
<ul style="list-style-type: disc;">
    <li>Copies files from path1 to path2</li>
    <li>Path1 and path2 can be file system locations or ftp locations</li>
    <li>To use ftp path write</li>
    <li>To upload using FTP you can write the location using like this</li>
</ul>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">ftp:[ftp user]/[ftp password]@[server name]:[ftp port]::[A:active,P:passive]::[target folder in ftp server]</p>
</div>
<h3 style='margin-top:2.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:16px;font-family:"Calibri Light",sans-serif;color:#1F3763;font-weight:normal;'>Restore Database</h3>
<div style='margin-top:0in;margin-right:.3in;margin-bottom:8.0pt;margin-left:.3in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;border:solid #8EAADB 1.0pt;padding:10.0pt 10.0pt 10.0pt 10.0pt;background:#DEEAF6;'>
    <p style="margin-top:0in;margin-right:.3in;margin-bottom:.0001pt;margin-left:.3in;background:#DEEAF6;border:none;padding:0in;font-size:13px;font-family:Consolas;color:black;margin:0in;">toolset -r [connection string] [database name] &quot;[backup file]&quot;</p>
</div>
<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:"Calibri",sans-serif;'>&nbsp;</p>