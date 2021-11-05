using UnityEngine;

namespace _Scripts
{
	public class Note : IEquipment
	{
		public readonly struct InitData
		{
			public readonly string Text;

			public InitData(string text)
			{
				Text = text;
			}
		}

		public IEquipment Init(InitData initData)
		{
			var text = initData.Text;

			var noteView = Object.Instantiate(GameManager.Instance.NoteViewPrefab, GameManager.Instance.Canvas.transform).GetComponent<NoteView>();

			noteView.Init(text);

			return this;
		}
	}
}