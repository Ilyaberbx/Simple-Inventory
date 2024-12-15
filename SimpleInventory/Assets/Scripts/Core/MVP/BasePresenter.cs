using System;
using UnityEngine;

namespace Core.MVP
{
    [RequireComponent(typeof(BaseView))]
    public abstract class BasePresenter : MonoBehaviour
    {
        protected BaseView DerivedView { get; private set; }
        protected IModel DerivedModel { get; private set; }

        private void Awake()
        {
            DerivedView = GetView();
        }

        public abstract void Rebuild();

        public virtual void SetDerivedModel(IModel value)
        {
            DerivedModel = value;
        }

        protected virtual BaseView GetView()
        {
            return GetComponent<BaseView>();
        }
    }

    public abstract class BasePresenter<TModel> : BasePresenter where TModel : IModel
    {
        protected TModel Model { get; private set; }
        public sealed override void SetDerivedModel(IModel value)
        {
            base.SetDerivedModel(value);

            if (value is TModel model)
            {
                SetModel(model);
            }
            else
                throw new InvalidCastException();
        }

        protected virtual void SetModel(TModel model)
        {
            Model = model;
        }
    }

    public abstract class BasePresenter<TView, TModel> : BasePresenter<TModel>
        where TModel : IModel
        where TView : BaseView
    {
        [SerializeField] private TView _view;

        protected TView View => _view;

        protected override BaseView GetView()
        {
            return View;
        }
    }
}