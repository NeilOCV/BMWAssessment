<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BMWAssessment._Default" %>

<%@ Register Src="~/UserControls/BarGraph.ascx" TagPrefix="uc1" TagName="BarGraph" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

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

                            <asp:TemplateField HeaderText="Destination" >
                                <ItemTemplate>
                                    <asp:Label Text='<%#Bind("DestinationFolder") %>' runat="server" ID="lblDestination" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Progress">
                                <ItemTemplate>
                                    <uc1:BarGraph runat="server" ID="ucBarGraph"  />
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
    <asp:Timer ID="tmrTimer" runat="server" Interval="1000" OnTick="tmrTimer_Tick">
                </asp:Timer>
    <div class="Container">
        <asp:Button Text="New Threaded Folder Sync Process" runat="server" CssClass="btn btn-danger" />
    </div>

</asp:Content>
