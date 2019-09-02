using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventarioAPI.Migrations
{
    public partial class Inventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    codigoCategoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.codigoCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    nit = table.Column<string>(nullable: false),
                    DPI = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.nit);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    codigoProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nit = table.Column<string>(nullable: false),
                    razonSocial = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true),
                    paginaWeb = table.Column<string>(nullable: true),
                    contactoPrincipal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.codigoProveedor);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmpaque",
                columns: table => new
                {
                    codigoEmpaque = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpaque", x => x.codigoEmpaque);
                });

            migrationBuilder.CreateTable(
                name: "EmailCliente",
                columns: table => new
                {
                    codigoEmail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(nullable: false),
                    nit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCliente", x => x.codigoEmail);
                    table.ForeignKey(
                        name: "FK_EmailCliente_Clientes_nit",
                        column: x => x.nit,
                        principalTable: "Clientes",
                        principalColumn: "nit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    numeroFactura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nit = table.Column<string>(nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.numeroFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Clientes_nit",
                        column: x => x.nit,
                        principalTable: "Clientes",
                        principalColumn: "nit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelefonoCliente",
                columns: table => new
                {
                    codigoTelefono = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numero = table.Column<string>(nullable: true),
                    descripcion = table.Column<string>(nullable: false),
                    nit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonoCliente", x => x.codigoTelefono);
                    table.ForeignKey(
                        name: "FK_TelefonoCliente_Clientes_nit",
                        column: x => x.nit,
                        principalTable: "Clientes",
                        principalColumn: "nit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    idCompra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numeroDocumento = table.Column<int>(nullable: false),
                    codigoProveedor = table.Column<int>(nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.idCompra);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_codigoProveedor",
                        column: x => x.codigoProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "codigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailProveedor",
                columns: table => new
                {
                    codigoEmail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(nullable: false),
                    codigoProveedor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProveedor", x => x.codigoEmail);
                    table.ForeignKey(
                        name: "FK_EmailProveedor_Proveedores_codigoProveedor",
                        column: x => x.codigoProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "codigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelefonoProveedor",
                columns: table => new
                {
                    codigoTelefono = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numero = table.Column<string>(nullable: true),
                    descripcion = table.Column<string>(nullable: false),
                    codigoProveedor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonoProveedor", x => x.codigoTelefono);
                    table.ForeignKey(
                        name: "FK_TelefonoProveedor_Proveedores_codigoProveedor",
                        column: x => x.codigoProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "codigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    codigoProducto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    codigoCategoria = table.Column<int>(nullable: false),
                    codigoEmpaque = table.Column<int>(nullable: false),
                    descripcion = table.Column<string>(nullable: false),
                    precioUnitario = table.Column<decimal>(nullable: false),
                    precioPorDocena = table.Column<decimal>(nullable: false),
                    precioPorMayor = table.Column<decimal>(nullable: false),
                    existencia = table.Column<int>(nullable: false),
                    imagen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.codigoProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Categoria_codigoCategoria",
                        column: x => x.codigoCategoria,
                        principalTable: "Categoria",
                        principalColumn: "codigoCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_TipoEmpaque_codigoEmpaque",
                        column: x => x.codigoEmpaque,
                        principalTable: "TipoEmpaque",
                        principalColumn: "codigoEmpaque",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    idDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idCompra = table.Column<int>(nullable: false),
                    codigoProducto = table.Column<int>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    precio = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.idDetalle);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Productos_codigoProducto",
                        column: x => x.codigoProducto,
                        principalTable: "Productos",
                        principalColumn: "codigoProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Compras_idCompra",
                        column: x => x.idCompra,
                        principalTable: "Compras",
                        principalColumn: "idCompra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
                columns: table => new
                {
                    codigoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numeroFactura = table.Column<int>(nullable: false),
                    codigoProducto = table.Column<int>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    precio = table.Column<decimal>(nullable: false),
                    descuento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura", x => x.codigoDetalle);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Productos_codigoProducto",
                        column: x => x.codigoProducto,
                        principalTable: "Productos",
                        principalColumn: "codigoProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura_numeroFactura",
                        column: x => x.numeroFactura,
                        principalTable: "Factura",
                        principalColumn: "numeroFactura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    codigoInventario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    codigoProducto = table.Column<int>(nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    tipoRegistro = table.Column<string>(nullable: false),
                    precio = table.Column<decimal>(nullable: false),
                    entradas = table.Column<int>(nullable: false),
                    salidas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.codigoInventario);
                    table.ForeignKey(
                        name: "FK_Inventario_Productos_codigoProducto",
                        column: x => x.codigoProducto,
                        principalTable: "Productos",
                        principalColumn: "codigoProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_codigoProveedor",
                table: "Compras",
                column: "codigoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_codigoProducto",
                table: "DetalleCompra",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_idCompra",
                table: "DetalleCompra",
                column: "idCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_codigoProducto",
                table: "DetalleFactura",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_numeroFactura",
                table: "DetalleFactura",
                column: "numeroFactura");

            migrationBuilder.CreateIndex(
                name: "IX_EmailCliente_nit",
                table: "EmailCliente",
                column: "nit");

            migrationBuilder.CreateIndex(
                name: "IX_EmailProveedor_codigoProveedor",
                table: "EmailProveedor",
                column: "codigoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_nit",
                table: "Factura",
                column: "nit");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_codigoProducto",
                table: "Inventario",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_codigoCategoria",
                table: "Productos",
                column: "codigoCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_codigoEmpaque",
                table: "Productos",
                column: "codigoEmpaque");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonoCliente_nit",
                table: "TelefonoCliente",
                column: "nit");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonoProveedor_codigoProveedor",
                table: "TelefonoProveedor",
                column: "codigoProveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "EmailCliente");

            migrationBuilder.DropTable(
                name: "EmailProveedor");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "TelefonoCliente");

            migrationBuilder.DropTable(
                name: "TelefonoProveedor");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "TipoEmpaque");
        }
    }
}
