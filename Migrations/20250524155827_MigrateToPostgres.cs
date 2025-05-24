using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class MigrateToPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "merchant_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchant_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_admin_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_admin_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plan_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    day_duration = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    features = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plan_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles_entity",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users_entity",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    verify_code = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venue_location_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_code = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    street_name = table.Column<string>(type: "text", nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    longitude = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_location_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_claims_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_entity_roles_entity_role_id",
                        column: x => x.role_id,
                        principalTable: "roles_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "merchant_user_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    merchant_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchant_user_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_merchant_user_entity_merchant_entity_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchant_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchant_user_entity_user_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    plan_id = table.Column<int>(type: "integer", nullable: false),
                    start_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    canceled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscription_entity_plan_entity_plan_id",
                        column: x => x.plan_id,
                        principalTable: "plan_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscription_entity_user_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_entity_users_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins_entity",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins_entity", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_entity_users_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles_entity",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles_entity", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_entity_roles_entity_role_id",
                        column: x => x.role_id,
                        principalTable: "roles_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_entity_users_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens_entity",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens_entity", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_entity_users_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venue_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_location_id = table.Column<int>(type: "integer", nullable: true),
                    merchant_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_venue_entity_merchant_entity_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchant_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_venue_entity_venue_location_entity_venue_location_id",
                        column: x => x.venue_location_id,
                        principalTable: "venue_location_entity",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "permission_menu_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    merchant_user_id = table.Column<int>(type: "integer", nullable: false),
                    permission_admin_id = table.Column<int>(type: "integer", nullable: false),
                    access = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_menu_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_permission_menu_entity_merchant_user_entity_merchant_user_id",
                        column: x => x.merchant_user_id,
                        principalTable: "merchant_user_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_menu_entity_permission_admin_entity_permission_a",
                        column: x => x.permission_admin_id,
                        principalTable: "permission_admin_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    enable_buy_anytime = table.Column<bool>(type: "boolean", nullable: false),
                    one_time_buy_limit = table.Column<int>(type: "integer", nullable: false),
                    can_buy_from = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    can_buy_until = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    enable_scan_anytime = table.Column<bool>(type: "boolean", nullable: false),
                    can_scan_from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    can_scan_until = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_entity_venue_entity_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venue_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_buy_window_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_buy_window_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_buy_window_entity_ticket_entity_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_purchase_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_purchase_id = table.Column<int>(type: "integer", nullable: false),
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    total = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    purchased_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_purchase_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_purchase_entity_ticket_entity_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_purchase_entity_user_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "users_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_scan_window_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_scan_window_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_scan_window_entity_ticket_entity_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_entry_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_purchase_id = table.Column<int>(type: "integer", nullable: false),
                    scan_code = table.Column<string>(type: "text", nullable: false),
                    is_scanned = table.Column<bool>(type: "boolean", nullable: false),
                    scanned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_entry_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_entry_entity_ticket_purchase_entity_ticket_purchase_",
                        column: x => x.ticket_purchase_id,
                        principalTable: "ticket_purchase_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_merchant_user_entity_merchant_id_user_id",
                table: "merchant_user_entity",
                columns: new[] { "merchant_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_merchant_user_entity_user_id",
                table: "merchant_user_entity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_menu_entity_merchant_user_id",
                table: "permission_menu_entity",
                column: "merchant_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_menu_entity_permission_admin_id",
                table: "permission_menu_entity",
                column: "permission_admin_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_entity_role_id",
                table: "role_claims_entity",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles_entity",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subscription_entity_plan_id",
                table: "subscription_entity",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_entity_user_id",
                table: "subscription_entity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_buy_window_entity_ticket_id",
                table: "ticket_buy_window_entity",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_entity_venue_id",
                table: "ticket_entity",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_entry_entity_ticket_purchase_id",
                table: "ticket_entry_entity",
                column: "ticket_purchase_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_purchase_entity_ticket_id",
                table: "ticket_purchase_entity",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_purchase_entity_user_id",
                table: "ticket_purchase_entity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_scan_window_entity_ticket_id",
                table: "ticket_scan_window_entity",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_entity_user_id",
                table: "user_claims_entity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_entity_user_id",
                table: "user_logins_entity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_entity_role_id",
                table: "user_roles_entity",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users_entity",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users_entity",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_entity_merchant_id",
                table: "venue_entity",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_entity_venue_location_id",
                table: "venue_entity",
                column: "venue_location_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission_menu_entity");

            migrationBuilder.DropTable(
                name: "role_claims_entity");

            migrationBuilder.DropTable(
                name: "subscription_entity");

            migrationBuilder.DropTable(
                name: "ticket_buy_window_entity");

            migrationBuilder.DropTable(
                name: "ticket_entry_entity");

            migrationBuilder.DropTable(
                name: "ticket_scan_window_entity");

            migrationBuilder.DropTable(
                name: "user_claims_entity");

            migrationBuilder.DropTable(
                name: "user_logins_entity");

            migrationBuilder.DropTable(
                name: "user_roles_entity");

            migrationBuilder.DropTable(
                name: "user_tokens_entity");

            migrationBuilder.DropTable(
                name: "merchant_user_entity");

            migrationBuilder.DropTable(
                name: "permission_admin_entity");

            migrationBuilder.DropTable(
                name: "plan_entity");

            migrationBuilder.DropTable(
                name: "ticket_purchase_entity");

            migrationBuilder.DropTable(
                name: "roles_entity");

            migrationBuilder.DropTable(
                name: "ticket_entity");

            migrationBuilder.DropTable(
                name: "users_entity");

            migrationBuilder.DropTable(
                name: "venue_entity");

            migrationBuilder.DropTable(
                name: "merchant_entity");

            migrationBuilder.DropTable(
                name: "venue_location_entity");
        }
    }
}
