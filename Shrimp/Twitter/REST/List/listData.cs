using System;
using System.Xml.Serialization;

namespace Shrimp.Twitter.REST.List
{
    /// <summary>
    /// リストの個別ID集
    /// </summary>
    public class listData : ICloneable
    {
        private decimal _list_id;
        private string _slug;
        private string _name;
        private decimal _create_user_id;

        public listData()
        {
        }

        public listData(decimal list_id, string slug, string name, decimal create_user_id)
        {
            this._list_id = list_id;
            this._slug = slug;
            this._name = name;
            this._create_user_id = create_user_id;
        }

        public object Clone()
        {
            var dest = new listData(_list_id, _slug, _name, _create_user_id);
            return dest;
        }

        /// <summary>
        /// リストID
        /// </summary>
        [XmlIgnore]
        public decimal list_id
        {
            get { return this._list_id; }
        }

        [XmlElement("list_id")]
        public decimal list_id_
        {
            get { return this._list_id; }
            set { this._list_id = value; }
        }

        /// <summary>
        /// リスト名
        /// </summary>
        [XmlIgnore]
        public string slug
        {
            get { return this._slug; }
        }

        [XmlElement("slug")]
        public string slug_
        {
            get { return this._slug; }
            set { this._slug = value; }
        }

        /// <summary>
        /// リスト名
        /// </summary>
        [XmlIgnore]
        public string name
        {
            get { return this._name; }
        }

        [XmlElement("name")]
        public string name_
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// 作成者のユーザーID
        /// </summary>
        [XmlIgnore]
        public decimal create_user_id
        {
            get { return this._create_user_id; }
        }

        [XmlElement("create_user_id")]
        public decimal create_user_id_
        {
            get { return this._create_user_id; }
            set { this._create_user_id = value; }
        }

    }
}
