using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YxSoft.Core.AOP;
using YxSoft.Core.DTO;

namespace YxSoft.Core
{
    public static class DTOHelper
    {
        /// <summary>
        /// 添加DTO用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="actionUser"></param>
        public static void setCreateState(this BaseObject dto, ActiveUser actionUser)
        {
            dto._FLAG_ = 1;
            dto._CT_ = DateTime.Now;
            dto._CUID_ = (int)actionUser.UserId;
            dto._CUTN_ = actionUser.TrueName;
            dto._UT_ = dto._CT_;
            dto._UUID_ = dto._CUID_;
            dto._UUTN_ = dto._CUTN_;
        }
        /// <summary>
        /// 更新DTO用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="actionUser">操作用户</param>
        public static void setUpdateState(this BaseObject dto, ActiveUser actionUser)
        {
            dto._FLAG_ = 2;
            dto._UT_ = DateTime.Now;
            dto._UUID_ = (int)actionUser.UserId;
            dto._UUTN_ = actionUser.TrueName;
        }

        /// <summary>
        /// 将DTO中的值设置到PO中
        /// </summary>
        /// <param name="dto">源标对象</param>
        /// <param name="po">目对象</param>
        /// <param name="colNameDTO">源列名</param>
        /// <param name="colNamePO">目标对象</param>
        public static void setValue(this BaseObject dto, object po, string colNameDTO, string colNamePO = "")
        {
            if (string.IsNullOrEmpty(colNamePO))
            {
                colNamePO = colNameDTO;
            }
            var dtoType = dto.GetType();
            var poType = po.GetType();
            var propertyDto = dtoType.GetProperty(colNameDTO);
            if (propertyDto == null)
            {
                return;
              //  throw new ArgumentException("未找到属性：" + colNameDTO);
            }
            var propertyPO = poType.GetProperty(colNamePO);
            if (propertyPO == null)
            {
                return;
                //  throw new ArgumentException("未找到属性：" + colNamePO);
            }
            //判断属性类型是否相等
            if (!EqualType(propertyDto.PropertyType, propertyPO.PropertyType))
            {
                throw new ArgumentException("类型不匹配，属性：" + colNameDTO + "->" + colNamePO);

            }
            var dtoMapping = propertyDto.GetFirstAttribute<DtoMappingAttribute>();
            if (dtoMapping == null)
            {
                throw new ArgumentException("未找到映射,属性：" + colNameDTO);
            }
            var displayName = propertyDto.Name;
            var attDisplay = propertyDto.GetFirstAttribute<DisplayNameAttribute>();
            if (attDisplay != null)
            {
                displayName = attDisplay.DisplayName;
            }
            var vDto = propertyDto.GetValue(dto, null);
            var vPo = propertyPO.GetValue(po, null);
            if (vDto == null && vPo != null && (propertyPO.PropertyType.Name == "Nullable`1" || propertyPO.PropertyType == typeof(string)))
            {
                dto.isDirty = true;
                if (dtoMapping.needLog && dto._FLAG_ == 2)
                {
                    dto._LOG_ += displayName + ":" + vPo + "->NULL" + "\n";
                }
                propertyPO.SetValue(po, vDto);
            }
            else if (vDto != null && !vDto.Equals(vPo))
            {
                dto.isDirty = true;
                if (dtoMapping.needLog && dto._FLAG_ == 2)
                {
                    dto._LOG_ += displayName + ":" + vPo + "修改为" + vDto + "\n";
                }
                else if (dtoMapping.needLog && dto._FLAG_ == 1)
                {
                    dto._LOG_ += displayName + ":" + vDto + "\n";
                }
                propertyPO.SetValue(po, vDto);
            }
        }
        /// <summary>
        /// 将DTO中的值设置到PO中
        /// </summary>
        /// <param name="dto">源标对象</param>
        /// <param name="po">目对象</param>
        public static void setValue(this BaseObject dto, object po)
        {
            var dtoType = dto.GetType();
            foreach (var propertyDto in dtoType.GetProperties())
            {
                var dtoMapping = propertyDto.GetFirstAttribute<DtoMappingAttribute>();
                if (dtoMapping != null)
                {
                    if (dtoMapping.Key == KeyFlag.Primary)
                    {
                        continue;
                    }
                    //如果dto为创建状态且创建有关需要写入

                    //判定PO是否允许写入
                    if ((dtoMapping.rwFlag & RWFlag.Write) != RWFlag.Write)
                    {
                        //创建状态允许写入
                        if (dto._FLAG_ == 1 && ("_CT_,_CUID_,_CUTN_".Contains(propertyDto.Name)))
                        {
                            //允许写入
                            var pname = propertyDto.Name;
                            var ename = dtoMapping.entityName;
                            dto.setValue(po, pname, ename);
                        }
                    }
                    else
                    {   //允许写入
                        var pname = propertyDto.Name;
                        var ename = dtoMapping.entityName;
                        dto.setValue(po, pname, ename);
                    }
                }
            }
        }

        /// <summary>
        /// 将PO中的值复制到DTO中
        /// </summary>
        /// <param name="dto">目标对象</param>
        /// <param name="po">源对象</param>
        /// <param name="colNameDTO">目标列名</param>
        /// <param name="colNamePO">源对象</param>
        public static void copyValue(this BaseObject dto, object po, string colNameDTO, string colNamePO = "")
        {
            if (string.IsNullOrEmpty(colNamePO))
            {
                colNamePO = colNameDTO;
            }
            var dtoType = dto.GetType();
            var poType = po.GetType();
            var propertyDto = dtoType.GetProperty(colNameDTO);
            if (propertyDto == null)
            {
                throw new ArgumentException("未找到属性：" + colNameDTO);
            }
            var propertyPO = poType.GetProperty(colNamePO);
            if (propertyPO == null)
            {
                throw new ArgumentException("未找到属性：" + colNamePO);
            }
            //判断属性类型是否相等
            if (propertyDto.PropertyType.Name != propertyPO.PropertyType.Name)
            {
                if (!EqualType(propertyDto.PropertyType, propertyPO.PropertyType)) { 
                 throw new ArgumentException("类型不匹配，属性：" + colNameDTO + "->" + colNamePO);
                }
                
            }
            var dtoMapping = propertyDto.GetFirstAttribute<DtoMappingAttribute>();
            if (dtoMapping == null)
            {
                throw new ArgumentException("未找到映射,属性：" + colNameDTO);
            }
            //var display = dtoMapping.displayName;
            //if (string.IsNullOrEmpty(display))
            //{
            //    display = propertyDto.Name;
            //}
            var vDto = propertyDto.GetValue(dto, null);
            var vPo = propertyPO.GetValue(po, null);
            if (vDto != null && vPo == null && (propertyDto.PropertyType.Name == "Nullable`1" || propertyDto.PropertyType == typeof(string)))
            {
                dto.isDirty = true;
                propertyDto.SetValue(dto, vPo, null);
            }
            else if (vPo != null && !vPo.Equals(vDto))
            {
                dto.isDirty = true;
                propertyDto.SetValue(dto, vPo, null);
            }
        }
        /// <summary>
        /// 将PO中的值复制到DTO中
        /// </summary>
        /// <param name="dto">目标对象</param>
        /// <param name="po">源对象</param>
        public static void copyValue(this BaseObject dto, object po)
        {
            var dtoType = dto.GetType();
            foreach (var propertyDto in dtoType.GetProperties())
            {
                var dtoMapping = propertyDto.GetFirstAttribute<DtoMappingAttribute>();
                if (dtoMapping != null)
                {
                    //判定PO是否允许读取
                    if ((dtoMapping.rwFlag & RWFlag.Read) != RWFlag.Read)
                    {
                        continue;
                    }
                    var dtoName = propertyDto.Name;
                    var poName = dtoMapping.entityName;
                    dto.copyValue(po, dtoName, poName);
                }
            }
        }
        
        public static object ParsePropertyValue(this PropertyInfo property, string value)
        {

            if (EqualType(property.PropertyType, typeof(DateTime)))
            {

                return Convert.ToDateTime(value);
            }
            else
            {
                var type = property.PropertyType;
                if (IsNullableType(type)) {
                    type = Nullable.GetUnderlyingType(type);
                }
                return Convert.ChangeType(value, type);
            }
        }

        /// <summary>
        /// 是否是可空类型
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type theType)
        {
            return theType.Name == "Nullable`1";
        }

        /// <summary>
        /// 类型比较
        /// </summary>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static bool EqualType(Type type1, Type type2)
        {

            if (IsNullableType(type1) && !IsNullableType(type2))
            {
                //type1是可空类型
                //var conver = new NullableConverter(type1);
                //conver.UnderlyingTypeConverter;
                return Nullable.GetUnderlyingType(type1) == type2;

            }
            else if (IsNullableType(type2) && !IsNullableType(type1))
            {
                //type2是可空类型
                return Nullable.GetUnderlyingType(type2) == type1;
            }
            else if (IsNullableType(type2) && IsNullableType(type1))
            {
                //都是可空类型
                return Nullable.GetUnderlyingType(type2) == Nullable.GetUnderlyingType(type1);
            }
            else
            {

                return type2 == type1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property">目标属性</param>
        /// <param name="ob">目标对象</param>
        /// <param name="vaule">目标值</param>
        public static void SetValue(this PropertyInfo property, object ob, object vaule)
        {
            //目标属性类型不可空
            if (!IsNullableType(property.PropertyType) && !property.PropertyType.IsClass && vaule == null)
            {
                //目标值类型为可空
                throw new ArgumentNullException("属性值不能为空："+ property.Name);
            }
            //如果是时间类型
            if (vaule != null && EqualType(vaule.GetType(), typeof(DateTime)))
            {
                DateTime dateTime;
                if (IsNullableType(vaule.GetType()))
                {
                    dateTime = ((DateTime?)vaule).GetValueOrDefault();
                }
                else
                {
                    dateTime = (DateTime)vaule;
                }
                if (dateTime == DateTime.MinValue)
                {
                    dateTime = DateTime.Now;
                }
                property.SetValue(ob, dateTime, null);
            }
            else
            {
                property.SetValue(ob, vaule, null);
            }
        }
    }
}
