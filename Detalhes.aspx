<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhes.aspx.cs" Inherits="CGE.SeletivaAnalista.Detalhes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="h-100">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.7.2/font/bootstrap-icons.css">
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
                <h5 class="m-3">
                    <i class="bi bi-person"></i>
                    <asp:Literal ID="litNome" runat="server" />
                </h5>

                <hr />

                <div class="card border-primary mb-3">
                    <div class="card-header">
                        <h5>
                            <i class="bi bi-patch-check"></i>
                            Experiências
                        </h5>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptExperiencia" runat="server">
                            <ItemTemplate>
                                <p>
                                    <strong>ID:</strong>
                                    <%# Eval("idexperiencia") %>
                                </p>
                                <p>
                                    <strong>Empresa:</strong>
                                    <%# Eval("empresa") %>
                                </p>
                                <p>
                                    <strong>Desc. Vaga:</strong>
                                    <%# Eval("desc_vagaperfilbarema") %>
                                            (<%# Eval("pontosPorAno_vagaperfilbarema") %> pontos por ano completo)
                                </p>
                                <p>
                                    <strong>Período: </strong><%# Eval("dtinicio","{0:dd/MM/yyyy}") %> à <%# Eval("dtfim","{0:dd/MM/yyyy}") %>
                                    <span class="badge bg-info"><%# Eval("anosCompletos") %> ano(s) completo(s)</span>
                                </p>

                                <p>
                                    <strong>Pontos:</strong>
                                    <strong class="text-decoration-underline"><%# Eval("pontuacao") %></strong>
                                    pontos "brutos" (não considerando o máximo)
                                </p>

                                <p>
                                    <strong>Documento:</strong>
                                    <a href="<%# "http://reserva.controladoria.mt.gov.br/processoseletivo/002/upload/" + Eval("documento_link") %>"
                                        target="_blank">Clique para visualizar
                                    </a>
                                </p>

                                <p>
                                    <strong>Descrição:</strong>
                                    <small>
                                        <%# Eval("descricao") %>
                                    </small>
                                </p>

                                <hr />

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="card-footer">
                        TOTAL DE PONTOS CONSIDERADOS:
                        <asp:Label ID="lblExperienciaTOTAL" runat="server" Font-Bold="true" />
                    </div>
                </div>

                <div class="card border-success mb-3">
                    <div class="card-header">
                        <h5>
                            <i class="bi bi-mortarboard"></i>
                            Certificados / Títulos
                        </h5>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptCertificacao" runat="server">
                            <ItemTemplate>
                                <p>
                                    <strong>Categoria:</strong>
                                    <%# Eval("desc_vagaperfilbarema") %>
                                            (<%# Eval("pontosPorItem_vagaperfilbarema") %> pontos por documento)
                                </p>
                                <p>
                                    <strong>Tipo:</strong>
                                    <%# Eval("tipo_documento") %>
                                </p>
                                <p>
                                    <strong>Descrição</strong>
                                    <%# Eval("nome_desc") %>
                                </p>

                                <p>
                                    <strong>Documento:</strong>
                                    <a href="<%# "http://reserva.controladoria.mt.gov.br/processoseletivo/002/upload/" + Eval("documento_link") %>"
                                        target="_blank">Clique para visualizar
                                    </a>
                                </p>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="card-footer">
                        TOTAL DE PONTOS CONSIDERADOS:
                        <asp:Label ID="lblCertificacaoTOTAL" runat="server" Font-Bold="true" />
                    </div>
                </div>

                <div class="alert alert-warning mb-3 text-end">
                    <h3>
                        Total de Pontos:
                        <asp:Literal ID="litTotal" runat="server" />
                    </h3>
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
