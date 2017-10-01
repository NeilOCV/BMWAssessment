<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Spec.aspx.cs" Inherits="BMWAssessment.Spec" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Assessment for BMW.</h3>
    <p>The following is a copy/paste of the specs as received.</p>
    <hr />
    <p class="MsoNormal">
        <b><span>Competency Test<o:p></o:p></span></b>
    </p>
    <p class="MsoNormal">
        <o:p>&nbsp;</o:p>
    </p>
    <p class="MsoNormal">
        <b>Problem Statement:<o:p></o:p></b>
    </p>
    <p class="MsoNormal">
        A system administrator needs to backup large folder structures after hours on a web server at a data centre. The sys admin know which folders to backup and what the destination is but he does not have remote access to the server as it is a shared services IIS server installation.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        Create a website for the sys admin where he can type both the source and destination folders and submit these to the IIS server. The IIS server must respond with a unique backup instruction id back to the sys admin. The webpage must periodically update a progress bar to show the progress of this backup instruction.
        <o:p></o:p>
    </p>
    <p class="MsoNormal">
        The sys admin may decide to close the website and reopen it later to check on the status of the backup instruction using the unique backup instruction id provided previously<o:p></o:p>
    </p>
    <p class="MsoNormal">
        You may assume that the IIS server has full access to the source and destination folder.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        <o:p>&nbsp;</o:p>
    </p>
    <p class="MsoNormal">
        <b>Requirements for the file backup process:<o:p></o:p></b>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: .0001pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>The complete source and destination folder, including, subfolders needs to be compared to determine which files need to be copied.<o:p></o:p>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: .0001pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>If a file is different in size or date it needs to be replaced in the destination folder.<o:p></o:p>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: .0001pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>If a file does not exist in the destination folder it needs to be copied.<o:p></o:p>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: .0001pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>If a file exists in the destination folder, but not in the source folder, it needs to be deleted from the destination folder.<o:p></o:p>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: .0001pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>If a sub folder file does not exist in the destination folder it needs to be created.<o:p></o:p>
    </p>
    <p style="text-indent: -18.0pt; mso-list: l0 level1 lfo1; line-height: 115%; font-size: 11.0pt; font-family: Calibri, sans-serif; margin-left: 36.0pt; margin-right: 0cm; margin-top: 0cm; margin-bottom: 10.0pt;">
        <![if !supportLists]><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]>If a sub folder file exists in the destination folder, but not in the source folder, it needs to be deleted from the destination folder.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        <b>Requirements for the HTML front end:<o:p></o:p></b>
    </p>
    <p class="MsoNormal">
        A page where the source and destination folders can be specified to initiate the backup operation.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        A page where the backup instruction id can be displayed (or queried) together with the status on a progress bar for that backup instruction.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        <o:p>&nbsp;</o:p>
    </p>
    <p class="MsoNormal">
        The code must be implemented as a mixture of C# backend, HTML5 + Javascript frontend. Commonly used frontend libraries may be utilised e.g. Jquery based, Angular, Bootstrap etc<o:p></o:p>
    </p>
    <p class="MsoNormal">
        Make the solution available on a Cloud shared drive so we can access the code.<o:p></o:p>
    </p>
    <p class="MsoNormal">
        Good luck!<o:p></o:p>
    </p>

</asp:Content>
