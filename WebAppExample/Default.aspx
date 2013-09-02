<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppExample.DefaultPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/Default.css" type="text/css" rel="stylesheet" />
    

</head>
<body>
    <form id="form1" runat="server">
        
        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableCdn="True" />
        
        <div>
            <asp:TextBox runat="server" ID="txtTest" Width="906px"></asp:TextBox>
            
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <p></p>
            </ContentTemplate>
            
        </asp:UpdatePanel>

    
       
    <div>
    <asp:UpdatePanel ID="gridUpdater" runat="server" UpdateMode="Conditional">
        
        <ContentTemplate>
            
        <asp:GridView runat="server"
            ID="GridConsultans"
            GridLines="None"
            DataKeyNames="ID"
            AllowPaging="True"
            AllowSorting="True"
            AutoGenerateColumns="False"
            DataSourceID="CustomDataSource"
            ShowHeaderWhenEmpty="True"
            >
            <Columns>
                <asp:TemplateField ControlStyle-Width="70">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblCmd" runat="server" Text="Commandi"></asp:Label></div>
                        <div>
                            <asp:LinkButton ID="linkFilter" runat="server" Text="filtra" OnClick="OnClickFilter" Height="25px"></asp:LinkButton>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="modifica" OnClick="OnEditingRow"/>
                        <asp:LinkButton ID="LinkButton4" runat="server" Text="cancella" OnClick="OnCancellingRow"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="aggiorna" OnClick="OnUpdatingRow"/>
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="annulla" OnClick="OnCancelEditingRow"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="80px" SortExpression="ID">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblID" runat="server" Text="ID"/></div>
                        <div>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="40%">
                                <asp:ListItem Value="" Text="" Enabled="True" Selected="True"></asp:ListItem>
                                <asp:ListItem Value=">" Text=">" Enabled="True"></asp:ListItem>
                                <asp:ListItem Value="<" Text="<" Enabled="True"></asp:ListItem>
                                <asp:ListItem Value="=" Text="=" Enabled="True"></asp:ListItem>
                             </asp:DropDownList>
                            <asp:TextBox ID="txtIDFilter" runat="server" Width="40%"></asp:TextBox>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblName" runat="server" Text="Nome" /></div>
                        <div>
                            <asp:TextBox ID="txtNameFilter" runat="server" Text='<%# Session["Name"] %>'></asp:TextBox>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblSurname" runat="server" Text="Cognome" /></div>
                        <div><asp:TextBox ID="txtSurnameFilter" runat="server" Text='<%# Session["Surname"] %>'></asp:TextBox></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text='<%# Eval("Surname") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="80">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblIdentityCode" runat="server" Text="Code" /></div>
                        <div><asp:TextBox ID="txtCodeFilter" runat="server" Text='<%# Session["IdentityCode"] %>'></asp:TextBox></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdentityCode" runat="server" Text='<%# Eval("IdentityCode") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="Label1" runat="server" Text="Num. Subagenti" Width="120"/></div>
                        <div><asp:TextBox ID="txtNumSubFilter" runat="server"></asp:TextBox></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Agents.Count") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="200">
                    <HeaderTemplate>
                        <div class="labelHeader"><asp:Label ID="lblEmail" runat="server" Text="Email Address" /></div>
                        <div>
                            <asp:TextBox ID="txtEmailFilter" Width="90%" runat="server" Text='<%# Session["Email"] %>'/>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' Width="120"/>
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        
        <nh:BusinessDataSource ID="CustomDataSource" runat="server"
            OnSelecting="CustomDataSource_Selecting"
            OnUpdating="CustomDataSource_Updating" TypeName="PersistentLayer.Domain.Salesman" OnInit="OnInitDataSource" OnExecutionQueryError="CustomDataSource_ExecutionQueryError">
            <SelectParameters>
                <nh:InnerControlParameter Name="Name" ControlID="GridConsultans" PropertyName="HeaderRow" InnerControlID="txtNameFilter" InnerPropertyName="Text" Type="String" />
                <nh:InnerControlParameter Name="Surname" ControlID="GridConsultans" PropertyName="HeaderRow" InnerControlID="txtSurnameFilter" InnerPropertyName="Text" Type="String" />
                <nh:InnerControlParameter Name="IdentityCode" ControlID="GridConsultans" PropertyName="HeaderRow" InnerControlID="txtCodeFilter" InnerPropertyName="Text" Type="Int32" />
                <nh:InnerControlParameter Name="Email" ControlID="GridConsultans" PropertyName="HeaderRow" InnerControlID="txtEmailFilter" InnerPropertyName="Text" Type="String" />
            </SelectParameters>

        </nh:BusinessDataSource>
            

        </ContentTemplate>

    </asp:UpdatePanel>
            
    </div>   
     

    

    
        
    </form>
</body>
</html>
