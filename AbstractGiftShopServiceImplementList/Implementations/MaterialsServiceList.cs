﻿using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;

namespace AbstractGiftShopServiceImplementList.Implementations
{
    public class MaterialsServiceList : IMaterialsService
    {
        private DataListSingleton source;
        public MaterialsServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MaterialsViewModel> GetList()
        {
            List<MaterialsViewModel> result = new List<MaterialsViewModel>();
            for (int i = 0; i < source.Materialss.Count; ++i)
            {
                result.Add(new MaterialsViewModel
                {
                    Id = source.Materialss[i].Id,
                    MaterialsName = source.Materialss[i].MaterialsName
                });
            }
            return result;
        }
        public MaterialsViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Materialss.Count; ++i)
            {
                if (source.Materialss[i].Id == id)
                {
                    return new MaterialsViewModel
                    {
                        Id = source.Materialss[i].Id,
                        MaterialsName = source.Materialss[i].MaterialsName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(MaterialsBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Materialss.Count; ++i)
            {
                if (source.Materialss[i].Id > maxId)
                {
                    maxId = source.Materialss[i].Id;
                }
                if (source.Materialss[i].MaterialsName == model.MaterialsName)
                {
                    throw new Exception("Уже есть материал с таким названием");
                }
            }
            source.Materialss.Add(new Materials
            {
                Id = maxId + 1,
                MaterialsName = model.MaterialsName
            });
        }
        public void UpdElement(MaterialsBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Materialss.Count; ++i)
            {
                if (source.Materialss[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Materialss[i].MaterialsName == model.MaterialsName &&
                source.Materialss[i].Id != model.Id)
                {
                    throw new Exception("Уже есть материал с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Materialss[index].MaterialsName = model.MaterialsName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Materialss.Count; ++i)
            {
                if (source.Materialss[i].Id == id)
                {
                    source.Materialss.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
