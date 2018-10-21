using System;
using System.Collections;

namespace JrscSoft.Common
{
	/// <summary>
	/// PreViewColumn ��ժҪ˵����
	/// </summary>
	public class PreViewColumn
	{
		public PreViewColumn()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string id;
		private int width;
		private string unit;
		private string align;
		private string fomat;
		private string caption;

		public string ID
		{
			get
			{
				return id;
			}
			set
			{
				id=value;
			}
		}

		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width=value;
			}
		}

		public string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				unit=value;
			}
		}

		public string Align
		{
			get
			{
				return align;
			}
			set
			{
				align=value;
			}
		}

		public string Fomat
		{
			get
			{
				return fomat;
			}
			set
			{
				fomat=value;
			}
		}

		public string Caption
		{
			get
			{
				return caption;
			}
			set
			{
				caption=value;
			}
		}
	}

	public class PreViewColumnCollection:CollectionBase
	{
		///<summary>
		///���캯����
		///</summary>
		public PreViewColumnCollection()
		{
			//
			//ToDo:��ӹ��캯���߼���
			//
		}
	
		///<summary>
		///InventoryInfo������
		///</summary>
		public PreViewColumn this[int index]
		{
			get
			{
				return (PreViewColumn)List[index];
			}
			set
			{
				List[index]=value;
			}
		}

		/// <summary>
		/// �򼯺������һ��PreViewColumn����
		/// </summary>
		/// <param name="value">һ��PreViewColumn�����ʵ��</param>
		/// <returns>��Ԫ���ڼ����е�λ������</returns>
		public int Add(PreViewColumn value)
		{
			return List.Add(value);
		}

		/// <summary>
		/// ȡ���ض�PreViewColumn���������
		/// </summary>
		/// <param name="value">һ��PreViewColumn�����ʵ��</param>
		/// <returns>����ҵ��ö����򷵻������������򣬷���-1</returns>
		public int IndexOf(PreViewColumn value)
		{
			return List.IndexOf(value);
		}

		/// <summary>
		/// �ڼ��ϵ��ض�λ���ϲ���һ��PreViewColumn����
		/// </summary>
		/// <param name="value">һ��PreViewColumn�����ʵ��</param>
		/// <param name="index">��0��ʼ������ֵ���ڸ�λ�ô�����һ������</param>
		public void Insert(PreViewColumn value,int index)
		{
			List.Insert(index,value);
		}

		/// <summary>
		/// �Ӽ�����ɾ��һ��PreViewColumn����
		/// </summary>
		/// <param name="value">һ��PreViewColumn�����ʵ��</param>
		public void Remove(PreViewColumn value)
		{
			List.Remove(value);
		}

		/// <summary>
		/// �жϼ������Ƿ�����ض�PreViewColumn����
		/// </summary>
		/// <param name="value">һ��PreViewColumn�����ʵ��</param>
		/// <returns>������ڸ�ʵ�����򷵻�true;���򷵻�false</returns>
		public bool Contains(PreViewColumn value)
		{
			return List.Contains(value);
		}
	}
}
