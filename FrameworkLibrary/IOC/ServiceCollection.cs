using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// Default implementation of <see cref="IServiceCollection"/>.
    /// </summary>
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();

        /// <inheritdoc />
        public int Count => _descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public ServiceDescriptor this[int index]
        {
            get
            {
                return _descriptors[index];
            }
            set
            {
                _descriptors[index] = value;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc />
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取所在位置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(ServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <summary>
        /// 指定插入添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Insert(int index, ServiceDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }
    }
    /// <summary>
    /// 容器接口
    /// </summary>
    public interface IServiceCollection : IList<ServiceDescriptor>
    {

    }
}
