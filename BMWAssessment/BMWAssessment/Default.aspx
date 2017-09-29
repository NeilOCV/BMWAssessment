<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BMWAssessment._Default" %>

<%@ Register Src="~/UserControls/BarGraph.ascx" TagPrefix="uc1" TagName="BarGraph" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Active Copy Threads On The Server</h1>
        <p class="lead">In this jumbotron, you can see all the active copy threads that is running on the server</p>
        <p>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdActiveThreads" runat="server" OnRowDataBound="grdActiveThreads_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="GUID ID">
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
            </asp:UpdatePanel>
        </p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
