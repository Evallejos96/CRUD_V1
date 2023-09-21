<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="CRUD.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="~/Principal.css" rel="stylesheet" />
    <link href="~/Content\bootstrap.min.css" rel="stylesheet" />
    <script src="~/Scripts\bootstrap.min.js"></script>

</head>


<body>

    <form id="form2" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <br />
        <br />
        <div class="form-control">
            <asp:Label ID="lblBuscarporNombre" runat="server" Text="Ingrese Nombre:" CssClass=""></asp:Label>&nbsp;
            <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar"></asp:TextBox>&nbsp;
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~\Resources\search.png" Height="30px" OnClick="btnBuscar_Click" />
        </div>
        <br />
        <br />
        <div class="container-md">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Salario" HeaderText="Salario" SortExpression="Salario" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~\Resources\editar.png" Height="30px"
                                CommandName="Editar" CommandArgument='<%# Eval("ID") %>'
                                OnCommand="btnEditar_Command" />
                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~\Resources\eliminar.png" Height="30px"
                                CommandName="Eliminar" CommandArgument='<%# Eval("ID") %>'
                                OnCommand="btnEliminar_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button Text="Agregar Nuevo" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" CssClass="btn btn-success" />
        </div>

        <asp:Panel ID="pnlAgregarEditar" runat="server" CssClass="modalPopup" Visible="false">
            <h2>Formulario</h2>
            <table class="auto-style1">
                <tr>
                    <td class="form-group">Nombre:&nbsp;
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblNombre" runat="server" Text="*Campo Nombre Incompleto" CssClass="campo-invalido" Visible="false"></asp:Label>

                    </td>
                    <td class="form-group">Apellido:&nbsp;
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblApellido" runat="server" Text="*Campo Apellido Incompleto" CssClass="campo-invalido" Visible="false"></asp:Label>
                    </td>
                    <td class="" rowspan="2">
                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="form-group">Email:&nbsp;&nbsp;
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblEmail" runat="server" Text="*Campo Email Incompleto" CssClass="campo-invalido" Visible="false"></asp:Label>
                    </td>
                    <td class="form-group">Salario:&nbsp;
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtSalario"/>
                        <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblSalario" runat="server" Text="*Campo salario Incompleto" CssClass="campo-invalido" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnGuardarNuevo" Text="Guardar" runat="server" CssClass="btn btn-success" OnClick="btnGuardarNuevo_Click" />
            <asp:Button ID="btnCerrarNuevo" runat="server" Text="Cerrar" CssClass="btn btn-danger" OnClick="btnCerrarNuevo_Click" />
        </asp:Panel>


        <asp:Panel ID="pnlNoResultados" runat="server" CssClass="modalPopup" Visible="false">
            <h2>No se Encontraron Resultados</h2>
            <br />
            <div class="align-items-sm-end">
                <asp:Button ID="btnCerrarNoEcontrados" runat="server" Text="Cerrar" CssClass="btn btn-danger" OnClick="btnCerrarNoEcontrados_Click" />
            </div>
        </asp:Panel>




    </form>

    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtAccion" runat="server" Visible="false"></asp:TextBox>




</body>




</html>
