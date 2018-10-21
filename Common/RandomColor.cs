using System;
using System.Drawing;
namespace JrscSoft.Common
{
/// <summary>
/// RandomColor类：生成随机颜色的类
/// </summary>
public class RandomColor
{
	//字段
	private Random random;		//随机数生成器对象
	private int seed;			//生成随机数的种子
	private int minAlpha;		//最小alpha分量
	private int maxAlpha;		//最大alpha分量
	private int minRed;		//最小红色分量
	private int maxRed;		//最大红色分量
	private int minGreen;		//最小绿色分量
	private int maxGreen;		//最大绿色分量
	private int minBlue;		//最小蓝色分量
	private int maxBlue;		//最大蓝色分量
	
	/// <summary>
	/// 构造函数
	/// </summary>
	public RandomColor()
		: this((int)DateTime.Now.Ticks, 0, 256, 0, 256, 0, 256, 0, 256)
	{
	}
	public RandomColor(int seed)
		: this(seed, 0, 256, 0, 256, 0, 256, 0, 256)
	{
	}
	public RandomColor(int seed, int minAlpha, int maxAlpha, int minRed, int maxRed, int minGreen, int maxGreen, int minBlue, int maxBlue)
	{
		//请在这里加入校验颜色分量大小的判断，如果小于0，大于256，则抛出异常。
		//因为颜色分量的范围大于等于0，小于等于255。
		this.seed = seed;
		random = new Random(seed);
		this.minAlpha = minAlpha;
		this.maxAlpha = maxAlpha;
		this.minRed = minRed;
		this.maxRed = maxRed;
		this.minGreen = minGreen;
		this.maxGreen = maxGreen;
		this.minBlue = minBlue;
		this.maxBlue = maxBlue;
	}

	/// <summary>
	/// 得到随机颜色
	/// </summary>
	/// <returns>返回生成的随机颜色</returns>
	public Color GetRandomColor()
	{
		int a, r, g, b;
		a = random.Next(minAlpha, maxAlpha);
		r = random.Next(minRed, maxRed);
		g = random.Next(minGreen, maxGreen);
		b = random.Next(minBlue, maxBlue);
		return Color.FromArgb(a, r, g, b);
	}

	/// <summary>
	/// 得到随机颜色数组
	/// </summary>
	/// <param name="count">数组的元素个数</param>
	/// <returns>返回生成的颜色数组</returns>
	public Color[] GetRandomColorArray(int count)
	{
		Color[] colors = new Color[count];
		for (int i = 0; i < count; i++)
			colors[i] = GetRandomColor();
		return colors;
	}
}
}