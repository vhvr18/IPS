<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Principal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSplitButton2 = New System.Windows.Forms.ToolStripSplitButton()
        Me.VentasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSplitButton3 = New System.Windows.Forms.ToolStripSplitButton()
        Me.PedidosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValorTotalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.SistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HistorialDeProductosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntradasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalidasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArticulosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AltaDeArtículoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntradaDeArtículosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalidasDeArtículosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UbicacionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificarCostoYPrecioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSplitButton2, Me.ToolStripSplitButton3, Me.ToolStripSplitButton1, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1184, 44)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.ToolStripButton3.Image = Global.IPS.My.Resources.Resources.carrito
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(182, 41)
        Me.ToolStripButton3.Text = "Punto de Venta"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.ToolStripButton4.Image = Global.IPS.My.Resources.Resources.check
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(171, 41)
        Me.ToolStripButton4.Text = "Check In/Out"
        '
        'ToolStripSplitButton2
        '
        Me.ToolStripSplitButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VentasToolStripMenuItem})
        Me.ToolStripSplitButton2.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.ToolStripSplitButton2.Image = Global.IPS.My.Resources.Resources.report
        Me.ToolStripSplitButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton2.Name = "ToolStripSplitButton2"
        Me.ToolStripSplitButton2.Size = New System.Drawing.Size(146, 41)
        Me.ToolStripSplitButton2.Text = "Reportes"
        '
        'VentasToolStripMenuItem
        '
        Me.VentasToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.VentasToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.ventas
        Me.VentasToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.VentasToolStripMenuItem.Name = "VentasToolStripMenuItem"
        Me.VentasToolStripMenuItem.Size = New System.Drawing.Size(153, 36)
        Me.VentasToolStripMenuItem.Text = "Ventas"
        '
        'ToolStripSplitButton3
        '
        Me.ToolStripSplitButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PedidosToolStripMenuItem, Me.InventarioToolStripMenuItem, Me.ValorTotalToolStripMenuItem})
        Me.ToolStripSplitButton3.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.ToolStripSplitButton3.Image = Global.IPS.My.Resources.Resources.task
        Me.ToolStripSplitButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton3.Name = "ToolStripSplitButton3"
        Me.ToolStripSplitButton3.Size = New System.Drawing.Size(170, 41)
        Me.ToolStripSplitButton3.Text = "Actividades"
        '
        'PedidosToolStripMenuItem
        '
        Me.PedidosToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.PedidosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.pedido
        Me.PedidosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.PedidosToolStripMenuItem.Name = "PedidosToolStripMenuItem"
        Me.PedidosToolStripMenuItem.Size = New System.Drawing.Size(191, 42)
        Me.PedidosToolStripMenuItem.Text = "Pedido"
        '
        'InventarioToolStripMenuItem
        '
        Me.InventarioToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.InventarioToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.Inventario
        Me.InventarioToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.InventarioToolStripMenuItem.Name = "InventarioToolStripMenuItem"
        Me.InventarioToolStripMenuItem.Size = New System.Drawing.Size(191, 42)
        Me.InventarioToolStripMenuItem.Text = "Inventario"
        '
        'ValorTotalToolStripMenuItem
        '
        Me.ValorTotalToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.ValorTotalToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.valor
        Me.ValorTotalToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ValorTotalToolStripMenuItem.Name = "ValorTotalToolStripMenuItem"
        Me.ValorTotalToolStripMenuItem.Size = New System.Drawing.Size(191, 42)
        Me.ValorTotalToolStripMenuItem.Text = "Valor Total"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SistemaToolStripMenuItem, Me.UsuariosToolStripMenuItem, Me.HistorialDeProductosToolStripMenuItem, Me.ArticulosToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.ToolStripSplitButton1.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripSplitButton1.Image = Global.IPS.My.Resources.Resources.Administración
        Me.ToolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(195, 41)
        Me.ToolStripSplitButton1.Text = "Administración"
        '
        'SistemaToolStripMenuItem
        '
        Me.SistemaToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SistemaToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.sistema
        Me.SistemaToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SistemaToolStripMenuItem.Name = "SistemaToolStripMenuItem"
        Me.SistemaToolStripMenuItem.Size = New System.Drawing.Size(281, 34)
        Me.SistemaToolStripMenuItem.Text = "Sistema"
        '
        'UsuariosToolStripMenuItem
        '
        Me.UsuariosToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsuariosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.usuariomenu
        Me.UsuariosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UsuariosToolStripMenuItem.Name = "UsuariosToolStripMenuItem"
        Me.UsuariosToolStripMenuItem.Size = New System.Drawing.Size(281, 34)
        Me.UsuariosToolStripMenuItem.Text = "Usuarios"
        '
        'HistorialDeProductosToolStripMenuItem
        '
        Me.HistorialDeProductosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntradasToolStripMenuItem, Me.SalidasToolStripMenuItem})
        Me.HistorialDeProductosToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HistorialDeProductosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.history
        Me.HistorialDeProductosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistorialDeProductosToolStripMenuItem.Name = "HistorialDeProductosToolStripMenuItem"
        Me.HistorialDeProductosToolStripMenuItem.Size = New System.Drawing.Size(281, 34)
        Me.HistorialDeProductosToolStripMenuItem.Text = "Historial de Productos"
        '
        'EntradasToolStripMenuItem
        '
        Me.EntradasToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.entradaA
        Me.EntradasToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.EntradasToolStripMenuItem.Name = "EntradasToolStripMenuItem"
        Me.EntradasToolStripMenuItem.Size = New System.Drawing.Size(160, 34)
        Me.EntradasToolStripMenuItem.Text = "Entrada"
        '
        'SalidasToolStripMenuItem
        '
        Me.SalidasToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.salidaA
        Me.SalidasToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SalidasToolStripMenuItem.Name = "SalidasToolStripMenuItem"
        Me.SalidasToolStripMenuItem.Size = New System.Drawing.Size(160, 34)
        Me.SalidasToolStripMenuItem.Text = "Salida"
        '
        'ArticulosToolStripMenuItem
        '
        Me.ArticulosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AltaDeArtículoToolStripMenuItem, Me.EntradaDeArtículosToolStripMenuItem, Me.SalidasDeArtículosToolStripMenuItem, Me.UbicacionesToolStripMenuItem, Me.ModificarCostoYPrecioToolStripMenuItem})
        Me.ArticulosToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArticulosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.barcode
        Me.ArticulosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ArticulosToolStripMenuItem.Name = "ArticulosToolStripMenuItem"
        Me.ArticulosToolStripMenuItem.Size = New System.Drawing.Size(281, 34)
        Me.ArticulosToolStripMenuItem.Text = "Artículos"
        '
        'AltaDeArtículoToolStripMenuItem
        '
        Me.AltaDeArtículoToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.registro
        Me.AltaDeArtículoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.AltaDeArtículoToolStripMenuItem.Name = "AltaDeArtículoToolStripMenuItem"
        Me.AltaDeArtículoToolStripMenuItem.Size = New System.Drawing.Size(295, 36)
        Me.AltaDeArtículoToolStripMenuItem.Text = "Alta de Artículo"
        '
        'EntradaDeArtículosToolStripMenuItem
        '
        Me.EntradaDeArtículosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.entra
        Me.EntradaDeArtículosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.EntradaDeArtículosToolStripMenuItem.Name = "EntradaDeArtículosToolStripMenuItem"
        Me.EntradaDeArtículosToolStripMenuItem.Size = New System.Drawing.Size(295, 36)
        Me.EntradaDeArtículosToolStripMenuItem.Text = "Entrada de Artículos"
        '
        'SalidasDeArtículosToolStripMenuItem
        '
        Me.SalidasDeArtículosToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.sal
        Me.SalidasDeArtículosToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SalidasDeArtículosToolStripMenuItem.Name = "SalidasDeArtículosToolStripMenuItem"
        Me.SalidasDeArtículosToolStripMenuItem.Size = New System.Drawing.Size(295, 36)
        Me.SalidasDeArtículosToolStripMenuItem.Text = "Salida de Artículos"
        '
        'UbicacionesToolStripMenuItem
        '
        Me.UbicacionesToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.bdlupa
        Me.UbicacionesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UbicacionesToolStripMenuItem.Name = "UbicacionesToolStripMenuItem"
        Me.UbicacionesToolStripMenuItem.Size = New System.Drawing.Size(295, 36)
        Me.UbicacionesToolStripMenuItem.Text = "Ubicaciones"
        '
        'ModificarCostoYPrecioToolStripMenuItem
        '
        Me.ModificarCostoYPrecioToolStripMenuItem.Image = Global.IPS.My.Resources.Resources.costos
        Me.ModificarCostoYPrecioToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ModificarCostoYPrecioToolStripMenuItem.Name = "ModificarCostoYPrecioToolStripMenuItem"
        Me.ModificarCostoYPrecioToolStripMenuItem.Size = New System.Drawing.Size(295, 36)
        Me.ModificarCostoYPrecioToolStripMenuItem.Text = "Modificar Costo y Precio"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(281, 34)
        Me.ToolStripMenuItem1.Text = "Tablas de Valores"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 44)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(70, 41)
        Me.ToolStripButton1.Text = "Salir"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1095, 714)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 711)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1184, 25)
        Me.ToolStrip2.TabIndex = 9
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(113, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.BackgroundImage = Global.IPS.My.Resources.Resources.fondo2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1184, 736)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.SystemColors.MenuText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1200, 726)
        Me.Name = "Principal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Integrated Pharmacy System(2.0)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripSplitButton1 As ToolStripSplitButton
    Friend WithEvents UsuariosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SistemaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents HistorialDeProductosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EntradasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalidasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ArticulosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AltaDeArtículoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EntradaDeArtículosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalidasDeArtículosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UbicacionesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripSplitButton2 As ToolStripSplitButton
    Friend WithEvents VentasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSplitButton3 As ToolStripSplitButton
    Friend WithEvents PedidosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InventarioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ValorTotalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ModificarCostoYPrecioToolStripMenuItem As ToolStripMenuItem
End Class
