<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BMWAssessment._Default" %>

<%@ Register Src="~/UserControls/BarGraph.ascx" TagPrefix="uc1" TagName="BarGraph" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Custom.css" rel="stylesheet" />

    <div class="jumbotron">
        <h1>Active Copy Threads On The Server</h1>
        <p class="lead">In this jumbotron, you can see all the active copy threads that is running on the server</p>
        <p>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdActiveThreads" runat="server" OnRowDataBound="grdActiveThreads_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="GUID ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Bind("ID") %>' runat="server" ID="lblID" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Bind("SourceFolder") %>' runat="server" ID="lblSource" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Destination">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Bind("DestinationFolder") %>' runat="server" ID="lblDestination" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Progress">
                                <ItemTemplate>
                                    <uc1:BarGraph runat="server" ID="ucBarGraph" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tmrTimer" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </p>
    </div>
    <asp:Timer ID="tmrTimer" runat="server" Interval="1000" OnTick="tmrTimer_Tick" Enabled="true">
    </asp:Timer>



    <div>

        <style type="text/css">
            
        </style>
        <a href="#openModal">New Thread</a>

        <div id="openModal" class="modalDialog" >
            <div style="width: 50%; height: 575px;">
                <%--<a href="#close" title="Close" class="close">X</a>--%>

                <h2>New Thread</h2>
                <h3>Select the source and the destination</h3>
                <h4>These folders are all relative to the server.  If the folder does not exist on the destination side, it will be created</h4>
                <asp:Panel runat="server">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center">
                                        <h4>Source</h4>
                                    </td>
                                    <td style="text-align: center">
                                        <h4>Destination</h4>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 50%; border-right-color: black; border-right-style: solid; border-right-width: 1px; padding: 25px;">

                                        <asp:Panel runat="server" Height="200px" Width="100%" ScrollBars="Both">
                                            <asp:TreeView ID="tvSource" runat="server" Height="200px" Width="100%" CssClass="form-control" PathSeparator="/" ShowLines="True" OnSelectedNodeChanged="tvSource_SelectedNodeChanged" ViewStateMode="Enabled" EnableViewState="true">
                                            </asp:TreeView>

                                        </asp:Panel>
                                        <asp:Label Text="Source Path" runat="server" /><br />
                                        <asp:TextBox runat="server" ID="txtSourcePath" CssClass="form-control" ReadOnly="true" Width="100%" />

                                    </td>
                                    <td style="width: 50%; padding: 25px;">

                                        <asp:Panel runat="server" Height="200px" Width="100%" ScrollBars="Both">
                                            <asp:TreeView ID="tvDestination" runat="server" Height="200px" Width="100%" CssClass="form-control" PathSeparator="/" ShowLines="True" OnSelectedNodeChanged="tvDestination_SelectedNodeChanged" ViewStateMode="Enabled" EnableViewState="true">
                                            </asp:TreeView>

                                        </asp:Panel>
                                        <asp:Label Text="Destination Path" runat="server" /><br />
                                        <asp:TextBox runat="server" ID="txtDestination" CssClass="form-control" ReadOnly="false" Width="100%" />

                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 50%; padding: 25px;"><asp:Button Text="Add" ID="btnAdd" runat="server" CssClass="btn btn-danger" Width="75" OnClick="btnAdd_Click" /></td>
                                    <td style="width: 50%; text-align:right; padding: 25px;"><asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-info" Width="75" /></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

            </div>
        </div>


    </div>

</asp:Content>
