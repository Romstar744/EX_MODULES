using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Экзамен
{
	public partial class Form1 : Form
	{
		private TextBox txtNumber;
		private DateTimePicker dtpDate;
		private TextBox txtPayer;
		private TextBox txtAmount;
		private ComboBox cmbPayerBank;
		private TextBox txtBIK;
		private TextBox txtINN;
		private ComboBox cmbPayeeBank;
		private TextBox txtPayeeAccount;
		private Button btnSubmit;
		private Button btnCancel;
		private RadioButton rbtnLegalEntity;
		private RadioButton rbtnEntrepreneur;
		public Form1()
		{
			InitializeComponent();
			InitializeComponents();
		}

		private void GenerateAutomaticValues()
		{
			// Генерация номера (пример: случайное число)
			Random random = new Random();
			txtNumber.Text = random.Next(100000, 999999).ToString();

			// Установка текущей даты
			dtpDate.Value = DateTime.Now;
		}

		private void InitializeComponents()
		{
			// Form setup, controls creation, layout, and event handler attachment

			// TextBox controls
			txtNumber = new TextBox();
			txtPayer = new TextBox();
			txtAmount = new TextBox();
			txtBIK = new TextBox();
			txtINN = new TextBox();
			txtPayeeAccount = new TextBox();
			

			// DateTimePicker control
			dtpDate = new DateTimePicker();

			// ComboBox controls
			cmbPayerBank = new ComboBox();
			cmbPayeeBank = new ComboBox();

			// Button controls
			btnSubmit = new Button();
			btnCancel = new Button();

			// RadioButton controls
			rbtnLegalEntity = new RadioButton();
			rbtnEntrepreneur = new RadioButton();

			// Set up form properties
			this.Controls.Add(new Label() { Text = "Number:", Location = new System.Drawing.Point(10, 20), Width = 100 });
			this.Controls.Add(txtNumber);

			this.Controls.Add(new Label() { Text = "Date:", Location = new System.Drawing.Point(10, 50), Width = 100 });
			this.Controls.Add(dtpDate);

			this.Controls.Add(new Label() { Text = "Payer:", Location = new System.Drawing.Point(10, 80), Width = 100 });
			this.Controls.Add(txtPayer);

			this.Controls.Add(new Label() { Text = "Amount:", Location = new System.Drawing.Point(10, 110), Width = 100 });
			this.Controls.Add(txtAmount);

			this.Controls.Add(new Label() { Text = "Payer Bank:", Location = new System.Drawing.Point(10, 140), Width = 100 });
			this.Controls.Add(cmbPayerBank);

			this.Controls.Add(new Label() { Text = "BIK (9 Numbers):", Location = new System.Drawing.Point(10, 170), Width = 100 });
			this.Controls.Add(txtBIK);

			this.Controls.Add(new Label() { Text = "INN (10-12 Numbers):", Location = new System.Drawing.Point(10, 200), Width = 150 });
			this.Controls.Add(txtINN);

			this.Controls.Add(new Label() { Text = "Entity or Entrepreneur:", Location = new System.Drawing.Point(10, 230), Width = 150 });
			this.Controls.Add(rbtnLegalEntity);
			this.Controls.Add(new Label() { Text = "Legal Entity", Location = new System.Drawing.Point(160, 260), Width = 100 });
			this.Controls.Add(rbtnEntrepreneur);
			this.Controls.Add(new Label() { Text = "Entrepreneur", Location = new System.Drawing.Point(260, 260), Width = 100 });

			this.Controls.Add(new Label() { Text = "Payee Bank:", Location = new System.Drawing.Point(10, 290), Width = 100 });
			this.Controls.Add(cmbPayeeBank);

			this.Controls.Add(new Label() { Text = "Payee Account (20 Num.):", Location = new System.Drawing.Point(10, 320), Width = 150 });
			this.Controls.Add(txtPayeeAccount);

			this.Controls.Add(btnSubmit);
			this.Controls.Add(btnCancel);

			// Set up control properties
			txtNumber.Location = new System.Drawing.Point(160, 20);
			dtpDate.Location = new System.Drawing.Point(160, 50);
			txtPayer.Location = new System.Drawing.Point(160, 80);
			txtAmount.Location = new System.Drawing.Point(160, 110);
			cmbPayerBank.Location = new System.Drawing.Point(160, 140);
			txtBIK.Location = new System.Drawing.Point(160, 170);
			txtINN.Location = new System.Drawing.Point(160, 200);

			rbtnLegalEntity.Location = new System.Drawing.Point(190, 230);
			rbtnLegalEntity.CheckedChanged += rbtnLegalEntity_CheckedChanged;

			rbtnEntrepreneur.Location = new System.Drawing.Point(290, 230);
			rbtnEntrepreneur.CheckedChanged += rbtnEntrepreneur_CheckedChanged;

			cmbPayeeBank.Location = new System.Drawing.Point(160, 290);
			txtPayeeAccount.Location = new System.Drawing.Point(160, 320);

			btnSubmit.Text = "Submit";
			btnSubmit.Location = new System.Drawing.Point(160, 350);
			btnSubmit.Click += btnSubmit_Click;

			btnCancel.Text = "Cancel";
			btnCancel.Location = new System.Drawing.Point(260, 350);
			btnCancel.Click += btnCancel_Click;

			
			// Set the default selection
			rbtnLegalEntity.Checked = true;

			// Заполнение ComboBox для банка плательщика
			cmbPayerBank.Items.AddRange(new string[] { "ПАО «Лучший банк»", "ПАО «Главный банк»", "ПАО «Замечательный банк»" });

			// Заполнение ComboBox для банка получателя (аналогично банку плательщика)
			cmbPayeeBank.Items.AddRange(new string[] { "ПАО «Лучший банк»", "ПАО «Главный банк»", "ПАО «Замечательный банк»" });

			GenerateAutomaticValues();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			// Проверка наличия всех обязательных полей
			if (AreAllFieldsFilled())
			{
				bool isBIKValid = IsBIKValid();
				bool isINNValid = IsINNValid();
				bool isPayeeAccountValid = IsPayeeAccountValid();

				if (isBIKValid && isINNValid && isPayeeAccountValid)
				{
					if (MessageBox.Show("Are you sure you want to submit the payment order?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						DisableFields();
						DisplayResult();
					}
				}
				else
				{
					if (!isBIKValid)
					{
						MessageBox.Show("Please enter a valid BIK with at least 9 digits.", "BIK Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					if (!isINNValid)
					{
						if (rbtnLegalEntity.Checked)
						{
							MessageBox.Show("Please enter a valid INN for a legal entity with 10 digits.", "INN Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else if (rbtnEntrepreneur.Checked)
						{
							MessageBox.Show("Please enter a valid INN for an entrepreneur with 12 digits.", "INN Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}

					if (!isPayeeAccountValid)
					{
						MessageBox.Show("Please enter a valid Payee Account with at least 20 digits.", "Payee Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private bool IsPayeeAccountValid()
		{
			// Проверка, что счет получателя содержит не менее 20 цифр
			return !string.IsNullOrWhiteSpace(txtPayeeAccount.Text) && txtPayeeAccount.Text.Length >= 20;
		}

		private bool IsBIKValid()
		{
			// Проверка, что БИК содержит не менее 9 цифр
			return !string.IsNullOrWhiteSpace(txtBIK.Text) && txtBIK.Text.Length >= 9;
		}

		private bool IsINNValid()
		{
			// Проверка, что ИНН содержит правильное количество цифр
			if (rbtnLegalEntity.Checked)
			{
				// Для юридического лица требуется 10 цифр
				return !string.IsNullOrWhiteSpace(txtINN.Text) && txtINN.Text.Length == 10;
			}
			else if (rbtnEntrepreneur.Checked)
			{
				// Для предпринимателя требуется 12 цифр
				return !string.IsNullOrWhiteSpace(txtINN.Text) && txtINN.Text.Length == 12;
			}

			return false;
		}


		private bool AreAllFieldsFilled()
		{
			// Проверка заполнения всех обязательных полей
			return !string.IsNullOrWhiteSpace(txtNumber.Text) &&
				   !string.IsNullOrWhiteSpace(txtPayer.Text) &&
				   !string.IsNullOrWhiteSpace(txtAmount.Text) &&
				   !string.IsNullOrWhiteSpace(cmbPayerBank.Text) &&
				   !string.IsNullOrWhiteSpace(txtBIK.Text) &&
				   !string.IsNullOrWhiteSpace(txtINN.Text) &&
				   !string.IsNullOrWhiteSpace(cmbPayeeBank.Text) &&
				   !string.IsNullOrWhiteSpace(txtPayeeAccount.Text);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to cancel data entry?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				ClearFields();
			}
		}

		private void rbtnLegalEntity_CheckedChanged(object sender, EventArgs e)
		{
			// Set validation for Legal Entity (10 digits)
			int requiredLength = 10;
			SetINNValidation(requiredLength);
		}

		private void rbtnEntrepreneur_CheckedChanged(object sender, EventArgs e)
		{
			// Set validation for Entrepreneur (12 digits)
			int requiredLength = 12;
			SetINNValidation(requiredLength);
		}

		private void SetINNValidation(int requiredLength)
		{
			txtINN.MaxLength = requiredLength;
			if (txtINN.Text.Length > requiredLength)
			{
				// If the current value is longer than the required length, truncate it
				txtINN.Text = txtINN.Text.Substring(0, requiredLength);
			}
		}

		private void DisableFields()
		{
			txtNumber.Enabled = false;
			dtpDate.Enabled = false;
			txtPayer.Enabled = false;
			txtAmount.Enabled = false;
			cmbPayerBank.Enabled = false;
			txtBIK.Enabled = false;
			txtINN.Enabled = false;
			cmbPayeeBank.Enabled = false;
			txtPayeeAccount.Enabled = false;
		}

		private void DisplayResult()
		{
			MessageBox.Show("Your payment order has been accepted for execution.\n\n" +
							$"Number: {txtNumber.Text}\n" +
							$"Date: {dtpDate.Value.ToShortDateString()}\n" +
							$"Payer: {txtPayer.Text}\n" +
							$"Amount: {txtAmount.Text}\n" +
							$"Payer Bank: {cmbPayerBank.Text}\n" +
							$"BIK: {txtBIK.Text}\n" +
							$"INN: {txtINN.Text}\n" +
							$"Payee Bank: {cmbPayeeBank.Text}\n" +
							$"Payee Account: {txtPayeeAccount.Text}\n"
							, "Result");
		}

		private void ClearFields()
		{
			txtNumber.Clear();
			dtpDate.Value = DateTime.Now;
			txtPayer.Clear();
			txtAmount.Clear();
			cmbPayerBank.SelectedIndex = -1;
			txtBIK.Clear();
			txtINN.Clear();
			cmbPayeeBank.SelectedIndex = -1;
			txtPayeeAccount.Clear();

			EnableFields();
		}

		private void EnableFields()
		{
			txtNumber.Enabled = true;
			dtpDate.Enabled = true;
			txtPayer.Enabled = true;
			txtAmount.Enabled = true;
			cmbPayerBank.Enabled = true;
			txtBIK.Enabled = true;
			txtINN.Enabled = true;
			cmbPayeeBank.Enabled = true;
			txtPayeeAccount.Enabled = true;
		}
	}
}
