namespace ExpressionMouse
{
    partial class ExpressionMouse
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressionMouse));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tbSmoothingFilter = new System.Windows.Forms.TextBox();
            this.nudConvolutionFilterLength = new System.Windows.Forms.NumericUpDown();
            this.nudEyeClosedFilterThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudDoubleClickSecondEyeThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudPercentageHorizontalEdgePixels = new System.Windows.Forms.NumericUpDown();
            this.nudBrowRaiserStartThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudHeadToScreenRelationYHeight = new System.Windows.Forms.NumericUpDown();
            this.nudMouthOpenStartThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudHeadToScreenRelationXWidth = new System.Windows.Forms.NumericUpDown();
            this.nudMouthOpenConfirmation = new System.Windows.Forms.NumericUpDown();
            this.nudScrollMultiplierDown = new System.Windows.Forms.NumericUpDown();
            this.nudScrollMultiplierUp = new System.Windows.Forms.NumericUpDown();
            this.nudMouthOpenEndThreshold = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.nudBrowLowererStartThreshold = new System.Windows.Forms.NumericUpDown();
            this.lbAction = new System.Windows.Forms.ListBox();
            this.ttClickDelay = new System.Windows.Forms.ToolTip(this.components);
            this.btReset = new System.Windows.Forms.Button();
            this.nudClickDelay = new System.Windows.Forms.NumericUpDown();
            this.ssInformation = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tiCheckStatus = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConvolutionFilterLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEyeClosedFilterThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoubleClickSecondEyeThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPercentageHorizontalEdgePixels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrowRaiserStartThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadToScreenRelationYHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenStartThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadToScreenRelationXWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenConfirmation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScrollMultiplierDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScrollMultiplierUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenEndThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrowLowererStartThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClickDelay)).BeginInit();
            this.ssInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your left Eye:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Your right Eye:";
            // 
            // btStart
            // 
            this.btStart.Enabled = false;
            this.btStart.Location = new System.Drawing.Point(20, 556);
            this.btStart.Margin = new System.Windows.Forms.Padding(4);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(140, 47);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(168, 556);
            this.btStop.Margin = new System.Windows.Forms.Padding(4);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(127, 47);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // pbLeft
            // 
            this.pbLeft.Location = new System.Drawing.Point(20, 21);
            this.pbLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(89, 59);
            this.pbLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLeft.TabIndex = 4;
            this.pbLeft.TabStop = false;
            // 
            // pbRight
            // 
            this.pbRight.Location = new System.Drawing.Point(166, 21);
            this.pbRight.Margin = new System.Windows.Forms.Padding(4);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(89, 59);
            this.pbRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRight.TabIndex = 5;
            this.pbRight.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Click Delay";
            this.ttClickDelay.SetToolTip(this.label3, "Timespan in Frames which have to elapse between two mouse actions");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Headrotation Smoothing Filter Values";
            this.ttClickDelay.SetToolTip(this.label4, "Weights for calculating weighted average of headrotation. Used for smoothing mous" +
        "e cursor.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 174);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(247, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Used Frames for closed eye detection";
            this.ttClickDelay.SetToolTip(this.label5, "More frames increases accuracy in closed eye detection, but it also increases the" +
        " lag between closing eye and mouseclick.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 270);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Brow Raiser Start Threshold";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 334);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Mouth Open Start Threshold";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 398);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 17);
            this.label10.TabIndex = 18;
            this.label10.Text = "Mouth Open End Threshold";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 430);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 17);
            this.label11.TabIndex = 19;
            this.label11.Text = "Scroll Multiplier Up";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 462);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 17);
            this.label12.TabIndex = 20;
            this.label12.Text = "Scroll Multiplier Down";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 494);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(214, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "HeadToScreenRelationX - Width";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 366);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(169, 17);
            this.label15.TabIndex = 23;
            this.label15.Text = "Mouth Open Confirmation";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 142);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(239, 17);
            this.label16.TabIndex = 24;
            this.label16.Text = "Percentage of horizontal edge Pixels";
            this.ttClickDelay.SetToolTip(this.label16, "Used for closed eye detection. More horizontal edge pixels mean a higher probabil" +
        "ity for closed eye.");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 238);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(234, 17);
            this.label17.TabIndex = 25;
            this.label17.Text = "Double Click Second Eye Threshold";
            this.ttClickDelay.SetToolTip(this.label17, "Threshold for second eye closing value. For differentiating between click and dou" +
        "ble click.");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 206);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(182, 17);
            this.label18.TabIndex = 26;
            this.label18.Text = "Eye Closed Filter Threshold";
            // 
            // tbSmoothingFilter
            // 
            this.tbSmoothingFilter.Location = new System.Drawing.Point(269, 107);
            this.tbSmoothingFilter.Margin = new System.Windows.Forms.Padding(4);
            this.tbSmoothingFilter.Name = "tbSmoothingFilter";
            this.tbSmoothingFilter.Size = new System.Drawing.Size(335, 22);
            this.tbSmoothingFilter.TabIndex = 28;
            this.tbSmoothingFilter.Text = "64, 55, 45, 35, 25, 20, 15, 10, 8, 7, 6, 5, 4, 3, 2, 1";
            // 
            // nudConvolutionFilterLength
            // 
            this.nudConvolutionFilterLength.Location = new System.Drawing.Point(269, 171);
            this.nudConvolutionFilterLength.Margin = new System.Windows.Forms.Padding(4);
            this.nudConvolutionFilterLength.Name = "nudConvolutionFilterLength";
            this.nudConvolutionFilterLength.Size = new System.Drawing.Size(336, 22);
            this.nudConvolutionFilterLength.TabIndex = 29;
            this.nudConvolutionFilterLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudEyeClosedFilterThreshold
            // 
            this.nudEyeClosedFilterThreshold.DecimalPlaces = 2;
            this.nudEyeClosedFilterThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudEyeClosedFilterThreshold.Location = new System.Drawing.Point(269, 203);
            this.nudEyeClosedFilterThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudEyeClosedFilterThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEyeClosedFilterThreshold.Name = "nudEyeClosedFilterThreshold";
            this.nudEyeClosedFilterThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudEyeClosedFilterThreshold.TabIndex = 30;
            this.nudEyeClosedFilterThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // nudDoubleClickSecondEyeThreshold
            // 
            this.nudDoubleClickSecondEyeThreshold.DecimalPlaces = 2;
            this.nudDoubleClickSecondEyeThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudDoubleClickSecondEyeThreshold.Location = new System.Drawing.Point(269, 235);
            this.nudDoubleClickSecondEyeThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudDoubleClickSecondEyeThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDoubleClickSecondEyeThreshold.Name = "nudDoubleClickSecondEyeThreshold";
            this.nudDoubleClickSecondEyeThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudDoubleClickSecondEyeThreshold.TabIndex = 31;
            this.nudDoubleClickSecondEyeThreshold.Value = new decimal(new int[] {
            18,
            0,
            0,
            131072});
            // 
            // nudPercentageHorizontalEdgePixels
            // 
            this.nudPercentageHorizontalEdgePixels.Location = new System.Drawing.Point(269, 139);
            this.nudPercentageHorizontalEdgePixels.Margin = new System.Windows.Forms.Padding(4);
            this.nudPercentageHorizontalEdgePixels.Name = "nudPercentageHorizontalEdgePixels";
            this.nudPercentageHorizontalEdgePixels.Size = new System.Drawing.Size(336, 22);
            this.nudPercentageHorizontalEdgePixels.TabIndex = 32;
            this.nudPercentageHorizontalEdgePixels.Value = new decimal(new int[] {
            68,
            0,
            0,
            0});
            // 
            // nudBrowRaiserStartThreshold
            // 
            this.nudBrowRaiserStartThreshold.DecimalPlaces = 2;
            this.nudBrowRaiserStartThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudBrowRaiserStartThreshold.Location = new System.Drawing.Point(269, 267);
            this.nudBrowRaiserStartThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudBrowRaiserStartThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrowRaiserStartThreshold.Name = "nudBrowRaiserStartThreshold";
            this.nudBrowRaiserStartThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudBrowRaiserStartThreshold.TabIndex = 33;
            this.nudBrowRaiserStartThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // nudHeadToScreenRelationYHeight
            // 
            this.nudHeadToScreenRelationYHeight.Location = new System.Drawing.Point(269, 523);
            this.nudHeadToScreenRelationYHeight.Margin = new System.Windows.Forms.Padding(4);
            this.nudHeadToScreenRelationYHeight.Name = "nudHeadToScreenRelationYHeight";
            this.nudHeadToScreenRelationYHeight.Size = new System.Drawing.Size(336, 22);
            this.nudHeadToScreenRelationYHeight.TabIndex = 34;
            this.nudHeadToScreenRelationYHeight.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // nudMouthOpenStartThreshold
            // 
            this.nudMouthOpenStartThreshold.DecimalPlaces = 2;
            this.nudMouthOpenStartThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudMouthOpenStartThreshold.Location = new System.Drawing.Point(269, 331);
            this.nudMouthOpenStartThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudMouthOpenStartThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMouthOpenStartThreshold.Name = "nudMouthOpenStartThreshold";
            this.nudMouthOpenStartThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudMouthOpenStartThreshold.TabIndex = 35;
            this.nudMouthOpenStartThreshold.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            // 
            // nudHeadToScreenRelationXWidth
            // 
            this.nudHeadToScreenRelationXWidth.Location = new System.Drawing.Point(269, 491);
            this.nudHeadToScreenRelationXWidth.Margin = new System.Windows.Forms.Padding(4);
            this.nudHeadToScreenRelationXWidth.Name = "nudHeadToScreenRelationXWidth";
            this.nudHeadToScreenRelationXWidth.Size = new System.Drawing.Size(336, 22);
            this.nudHeadToScreenRelationXWidth.TabIndex = 40;
            this.nudHeadToScreenRelationXWidth.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            // 
            // nudMouthOpenConfirmation
            // 
            this.nudMouthOpenConfirmation.DecimalPlaces = 2;
            this.nudMouthOpenConfirmation.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudMouthOpenConfirmation.Location = new System.Drawing.Point(269, 363);
            this.nudMouthOpenConfirmation.Margin = new System.Windows.Forms.Padding(4);
            this.nudMouthOpenConfirmation.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMouthOpenConfirmation.Name = "nudMouthOpenConfirmation";
            this.nudMouthOpenConfirmation.Size = new System.Drawing.Size(336, 22);
            this.nudMouthOpenConfirmation.TabIndex = 39;
            this.nudMouthOpenConfirmation.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // nudScrollMultiplierDown
            // 
            this.nudScrollMultiplierDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudScrollMultiplierDown.Location = new System.Drawing.Point(269, 459);
            this.nudScrollMultiplierDown.Margin = new System.Windows.Forms.Padding(4);
            this.nudScrollMultiplierDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudScrollMultiplierDown.Name = "nudScrollMultiplierDown";
            this.nudScrollMultiplierDown.Size = new System.Drawing.Size(336, 22);
            this.nudScrollMultiplierDown.TabIndex = 38;
            this.nudScrollMultiplierDown.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // nudScrollMultiplierUp
            // 
            this.nudScrollMultiplierUp.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudScrollMultiplierUp.Location = new System.Drawing.Point(269, 427);
            this.nudScrollMultiplierUp.Margin = new System.Windows.Forms.Padding(4);
            this.nudScrollMultiplierUp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudScrollMultiplierUp.Name = "nudScrollMultiplierUp";
            this.nudScrollMultiplierUp.Size = new System.Drawing.Size(336, 22);
            this.nudScrollMultiplierUp.TabIndex = 37;
            this.nudScrollMultiplierUp.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // nudMouthOpenEndThreshold
            // 
            this.nudMouthOpenEndThreshold.DecimalPlaces = 2;
            this.nudMouthOpenEndThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMouthOpenEndThreshold.Location = new System.Drawing.Point(269, 395);
            this.nudMouthOpenEndThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudMouthOpenEndThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMouthOpenEndThreshold.Name = "nudMouthOpenEndThreshold";
            this.nudMouthOpenEndThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudMouthOpenEndThreshold.TabIndex = 36;
            this.nudMouthOpenEndThreshold.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(635, 55);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(107, 17);
            this.label19.TabIndex = 43;
            this.label19.Text = "Action Protocol:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 302);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(196, 17);
            this.label20.TabIndex = 44;
            this.label20.Text = "Brow Lowerer Start Threshold";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(16, 526);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(219, 17);
            this.label21.TabIndex = 45;
            this.label21.Text = "HeadToScreenRelationY - Height";
            // 
            // nudBrowLowererStartThreshold
            // 
            this.nudBrowLowererStartThreshold.DecimalPlaces = 2;
            this.nudBrowLowererStartThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudBrowLowererStartThreshold.Location = new System.Drawing.Point(269, 299);
            this.nudBrowLowererStartThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.nudBrowLowererStartThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrowLowererStartThreshold.Name = "nudBrowLowererStartThreshold";
            this.nudBrowLowererStartThreshold.Size = new System.Drawing.Size(336, 22);
            this.nudBrowLowererStartThreshold.TabIndex = 46;
            this.nudBrowLowererStartThreshold.Value = new decimal(new int[] {
            16,
            0,
            0,
            131072});
            // 
            // lbAction
            // 
            this.lbAction.FormattingEnabled = true;
            this.lbAction.ItemHeight = 16;
            this.lbAction.Location = new System.Drawing.Point(639, 79);
            this.lbAction.Margin = new System.Windows.Forms.Padding(4);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(443, 468);
            this.lbAction.TabIndex = 47;
            // 
            // ttClickDelay
            // 
            this.ttClickDelay.Popup += new System.Windows.Forms.PopupEventHandler(this.ttClickDelay_Popup);
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(303, 555);
            this.btReset.Margin = new System.Windows.Forms.Padding(4);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(139, 48);
            this.btReset.TabIndex = 51;
            this.btReset.Text = "Reset values";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // nudClickDelay
            // 
            this.nudClickDelay.Location = new System.Drawing.Point(269, 79);
            this.nudClickDelay.Margin = new System.Windows.Forms.Padding(4);
            this.nudClickDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudClickDelay.Name = "nudClickDelay";
            this.nudClickDelay.Size = new System.Drawing.Size(336, 22);
            this.nudClickDelay.TabIndex = 52;
            this.nudClickDelay.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ssInformation
            // 
            this.ssInformation.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.ssInformation.Location = new System.Drawing.Point(0, 607);
            this.ssInformation.Name = "ssInformation";
            this.ssInformation.Size = new System.Drawing.Size(1099, 33);
            this.ssInformation.TabIndex = 53;
            this.ssInformation.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(331, 28);
            this.lbStatus.Text = "Initializing Kinect... Looking for face...";
            // 
            // tiCheckStatus
            // 
            this.tiCheckStatus.Enabled = true;
            this.tiCheckStatus.Interval = 2000;
            this.tiCheckStatus.Tick += new System.EventHandler(this.tiCheckStatus_Tick);
            // 
            // ExpressionMouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 640);
            this.Controls.Add(this.ssInformation);
            this.Controls.Add(this.nudClickDelay);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.lbAction);
            this.Controls.Add(this.nudBrowLowererStartThreshold);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.nudHeadToScreenRelationXWidth);
            this.Controls.Add(this.nudMouthOpenConfirmation);
            this.Controls.Add(this.nudScrollMultiplierDown);
            this.Controls.Add(this.nudScrollMultiplierUp);
            this.Controls.Add(this.nudMouthOpenEndThreshold);
            this.Controls.Add(this.nudMouthOpenStartThreshold);
            this.Controls.Add(this.nudHeadToScreenRelationYHeight);
            this.Controls.Add(this.nudBrowRaiserStartThreshold);
            this.Controls.Add(this.nudPercentageHorizontalEdgePixels);
            this.Controls.Add(this.nudDoubleClickSecondEyeThreshold);
            this.Controls.Add(this.nudEyeClosedFilterThreshold);
            this.Controls.Add(this.nudConvolutionFilterLength);
            this.Controls.Add(this.tbSmoothingFilter);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbRight);
            this.Controls.Add(this.pbLeft);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExpressionMouse";
            this.Text = "Kinect ExpressionMouse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceMouseConfig_FormClosing);
            this.Load += new System.EventHandler(this.FaceMouseConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConvolutionFilterLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEyeClosedFilterThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoubleClickSecondEyeThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPercentageHorizontalEdgePixels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrowRaiserStartThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadToScreenRelationYHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenStartThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadToScreenRelationXWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenConfirmation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScrollMultiplierDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScrollMultiplierUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMouthOpenEndThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrowLowererStartThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClickDelay)).EndInit();
            this.ssInformation.ResumeLayout(false);
            this.ssInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.PictureBox pbRight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbSmoothingFilter;
        private System.Windows.Forms.NumericUpDown nudConvolutionFilterLength;
        private System.Windows.Forms.NumericUpDown nudEyeClosedFilterThreshold;
        private System.Windows.Forms.NumericUpDown nudDoubleClickSecondEyeThreshold;
        private System.Windows.Forms.NumericUpDown nudPercentageHorizontalEdgePixels;
        private System.Windows.Forms.NumericUpDown nudBrowRaiserStartThreshold;
        private System.Windows.Forms.NumericUpDown nudHeadToScreenRelationYHeight;
        private System.Windows.Forms.NumericUpDown nudMouthOpenStartThreshold;
        private System.Windows.Forms.NumericUpDown nudHeadToScreenRelationXWidth;
        private System.Windows.Forms.NumericUpDown nudMouthOpenConfirmation;
        private System.Windows.Forms.NumericUpDown nudScrollMultiplierDown;
        private System.Windows.Forms.NumericUpDown nudScrollMultiplierUp;
        private System.Windows.Forms.NumericUpDown nudMouthOpenEndThreshold;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudBrowLowererStartThreshold;
        private System.Windows.Forms.ListBox lbAction;
        private System.Windows.Forms.ToolTip ttClickDelay;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.NumericUpDown nudClickDelay;
        private System.Windows.Forms.StatusStrip ssInformation;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.Timer tiCheckStatus;
    }
}

