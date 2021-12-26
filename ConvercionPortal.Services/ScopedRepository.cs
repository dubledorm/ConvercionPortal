﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public class ScopedRepository<TModel> : Dictionary<string, object>
    {
        // Словарь для связки свойств модели(view) с именами scope 
        private Dictionary<string, Func<string>> PropScopeRelations { get; set; }

        public ScopedRepository() : base()
        {
            PropScopeRelations = new Dictionary<string,Func<string>>();
        }

        // Добавить новую связь scope и свойства модели для автоматического построения запроса при
        // заполнении заполнении свйств
        public void AddScopeRelation(string ScopeName, Func<string> PropertyGetter) 
        {
            PropScopeRelations.Add(ScopeName, PropertyGetter);
        }


        // Добавить новый scope.
        // Это scope на уровне реализации репозитория. Связь имени scope и функции, добавляющей условие в фильтр
        protected delegate IQueryable<TModel> ScopeFunction(IQueryable<TModel> query, string value);
        protected ScopedRepository<TModel> AddScope(string ScopeName, ScopeFunction Func)
        {
            this.Add(ScopeName, Func);
            return this;
        }

        // Добавить фильтры в переданный запрос.
        // Фильтр добавляется если заполнено свойство модели, переденнное в словарь PropScopeRelations через PropertyGetter
        // Если свойство имеет значение, то в словаре PropScopeRelations берётся ключ для него и по этому же ключу ищется 
        // функция ScopeFunction для добавления фильтра.
        protected IQueryable<TModel> ExtendQueryByFilter(IQueryable<TModel> query)
        {
            foreach (var item in PropScopeRelations)
                if (!string.IsNullOrWhiteSpace(item.Value()))
                {
                    Object fnc;
                    this.TryGetValue(item.Key, out fnc);
                    query = (fnc as ScopeFunction)(query, item.Value());

                }
            return query;
        }
    }
}