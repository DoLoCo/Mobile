﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class BaseViewModel<T> : BaseViewModel
            where T : class, new()
    {
        static readonly MethodInfo GetDependency;

        static BaseViewModel()
        {
            var repoType = App.TypeMap[typeof(T)];
            var getMethod = typeof(DependencyService)
                .GetRuntimeMethods()
                .Single((method) =>
                    method.Name.Equals("Get"));
            GetDependency = getMethod.MakeGenericMethod(repoType);
        }

        const string IconFormat = "{0}.png";

        public BaseViewModel()
        {
            IsLoading = false;
            Title = typeof(T).Name;
            Icon = string.Format(IconFormat, Title).ToLower();
            Models = new System.Collections.ObjectModel.ObservableCollection<T>();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Models { get; private set; }

        private Command loadModelsCommand;
        public Command LoadModelsCommand
        {
            get
            {
                return loadModelsCommand ?? (loadModelsCommand = new Command(ExecuteLoadModelsCommand));
            }
        }

        protected virtual async void ExecuteLoadModelsCommand()
        {

            using (var service = (IRepository<T>)GetDependency.Invoke(null, new object[] { DependencyFetchTarget.GlobalInstance }))
            {
                var items = await service.All();

                foreach (var item in items)
                    Models.Add(item);
            }
            OnPropertyChanged("Models");
        }
    }

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        private bool isLoading = false;
        public const string IsLoadingPropertyName = "IsLoading";
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                SetProperty(ref isLoading, value, IsLoadingPropertyName);
            }
        }

        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value, TitlePropertyName); }
        }

        private string subTitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";
        public string Subtitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
        }

        private string icon = null;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value, IconPropertyName); }
        }

        protected void SetProperty<U>(
            ref U backingStore, U value,
            string propertyName,
            Action onChanged = null,
            Action<U> onChanging = null)
        {


            if (EqualityComparer<U>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            OnPropertyChanging(propertyName);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanging implementation
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging == null)
                return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
