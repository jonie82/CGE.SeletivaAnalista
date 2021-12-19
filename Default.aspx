<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CGE.SeletivaAnalista.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="h-100">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.6.1/font/bootstrap-icons.css">
    <title>Seletiva Analistas CGE</title>

    <style>
        .bd-placeholder-img {
            font-size: 1.125rem;
            text-anchor: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            user-select: none;
        }

        @media (min-width: 768px) {
            .bd-placeholder-img-lg {
                font-size: 3.5rem;
            }
        }

        .container {
            width: auto;
            max-width: 800px;
            padding: 0 15px;
        }
    </style>
</head>
<body class="d-flex flex-column h-100">

    <main class="flex-shrink-0">
        <div class="container">
            <form id="form1" runat="server">

                <h2 class="mt-5">Seletiva de Analistas de Sistemas</h2>


                <div class="alert alert-light border">
                    <div class="row">
                        <div class="col-md-6">
                            <h4>
                                <i class="bi bi-table"></i>
                                Tabela de Barema
                            </h4>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <div class="input-group-text">
                                    <i class="bi bi-sort-down"></i>
                                </div>

                                <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Ordenar por Nome" />
                                    <asp:ListItem Text="Ordenar por Pontos (Decresc.)" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>

                <asp:GridView ID="gvDados" runat="server" CssClass="table table-striped" GridLines="None" AutoGenerateColumns="false"
                    DataKeyNames="idanalista" OnSelectedIndexChanged="gvDados_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandName="select" CssClass="btn btn-outline-secondary btn-sm py-0">
                                    detalhes
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nomeAnalista" HeaderText="Nome" />
                        <asp:BoundField DataField="pontosExperienciaConsiderados" HeaderText="Pts. Experiência" />
                        <asp:BoundField DataField="pontosCertificacaoConsiderados" HeaderText="Pts. Certificação" />
                        <asp:BoundField DataField="pontosTotais" HeaderText="TOTAL" ItemStyle-Font-Bold="true"
                            ItemStyle-CssClass="border-start" />
                    </Columns>
                </asp:GridView>

                <div class="alert alert-secondary">
                    <i class="bi bi-info-circle-fill"></i>
                    Exibindo
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                    registros
                </div>
            </form>

        </div>
    </main>

    <footer class="footer mt-auto py-3 bg-primary">
        <div class="container">
            <span class="text-white">CGE - Controladoria Geral do Estado
            </span>
        </div>
    </footer>

</body>
</html>
