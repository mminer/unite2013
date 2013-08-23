using System;
using System.Security;
using System.Runtime.InteropServices;

public class SecureScore
{
	SecureString container = new SecureString();
	int? _val;

	public int val
	{
		get {
			if (!_val.HasValue) {
				_val = GetScore(container);
			}

			return _val.Value;
		}
	}

	public int finalVal { get { return GetScore(container); } }

	public SecureScore () : this(0) {}

	public SecureScore (int val)
	{
		SetScore(container, val);
	}

	SecureScore (SecureScore score)
	{
		this.container = score.container.Copy();
	}

	void UpdateScore (int difference)
	{
		var newVal = GetScore(container) + difference;
		SetScore(container, newVal);
		_val = newVal;
	}

	#region Operator Overloads

	public override bool Equals (object obj)
	{
		// If parameter is null return false.
		if (obj == null) {
			return false;
		}

		// If parameter cannot be cast to SecureScore return false.
		var score = obj as SecureScore;

		if (score == null) {
			return false;
		}

		// Return true if the scores match.
		return this.finalVal == score.finalVal;
	}

	public override int GetHashCode()
    {
        return this.finalVal;
    }

	public static SecureScore operator + (SecureScore score, int difference)
    {
		var newScore = new SecureScore(score);
		newScore.UpdateScore(difference);
		return newScore;
    }

	public static SecureScore operator - (SecureScore score, int difference)
    {
		var newScore = new SecureScore(score);
		newScore.UpdateScore(-difference);
		return newScore;
    }

    public static SecureScore operator ++ (SecureScore score)
    {
		score.UpdateScore(1);
		return score;
    }

    public static SecureScore operator -- (SecureScore score)
    {
		score.UpdateScore(-1);
		return score;
    }

	public static bool operator <  (SecureScore score, int val) { return score.finalVal < val; }
	public static bool operator >  (SecureScore score, int val) { return score.finalVal > val; }
	public static bool operator <= (SecureScore score, int val) { return score.finalVal <= val; }
	public static bool operator >= (SecureScore score, int val) { return score.finalVal >= val; }
	public static bool operator == (SecureScore score, int val) { return score.finalVal == val; }
	public static bool operator != (SecureScore score, int val) { return score.finalVal != val; }

	#endregion

	static int GetScore (SecureString container)
	{
		var bstr = Marshal.SecureStringToBSTR(container);
		var scoreStr = Marshal.PtrToStringBSTR(bstr);
		Marshal.FreeBSTR(bstr);
		var score = int.Parse(scoreStr);
		return score;
	}

	static void SetScore (SecureString container, int val)
	{
		container.Clear();
		var chars = val.ToString().ToCharArray();

		foreach (var c in chars) {
			container.AppendChar(c);
		}
	}
}
