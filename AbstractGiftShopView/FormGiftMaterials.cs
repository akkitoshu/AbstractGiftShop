﻿using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;


namespace AbstractGiftShopView
{
    public partial class FormGiftMaterials : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public GiftMaterialsViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly IMaterialsService service;
        private GiftMaterialsViewModel model;
        public FormGiftMaterials(IMaterialsService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormGiftMaterials_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxMaterial.DisplayMember = "MaterialsName";
                    comboBoxMaterial.ValueMember = "Id";
                    comboBoxMaterial.DataSource = list;
                    comboBoxMaterial.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxMaterial.Enabled = false;
                comboBoxMaterial.SelectedValue = model.MaterialsId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterial.SelectedValue == null)
            {
                MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new GiftMaterialsViewModel
                    {
                        MaterialsId = Convert.ToInt32(comboBoxMaterial.SelectedValue),
                        MaterialsName = comboBoxMaterial.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
