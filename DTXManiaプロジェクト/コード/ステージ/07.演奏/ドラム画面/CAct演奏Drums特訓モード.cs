﻿using FDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace DTXMania
{
	class CAct演奏Drums特訓モード : CActivity
	{
		public CAct演奏Drums特訓モード()
		{
			base.b活性化してない = true;
		}

		public override void On活性化()
		{
			this.n現在の小節線 = 0;
			this.b特訓PAUSE = false;
			this.n最終演奏位置ms = 0;

			base.On活性化();

			CDTX dTX = CDTXMania.DTX;

			var measureCount = 1;
			var bIsInGoGo = false;

			int endtime = 1;
			int bgmlength = 1;

			for (int index = 0; index < CDTXMania.DTX.listChip.Count; index++)
			{
				if (CDTXMania.DTX.listChip[index].nチャンネル番号 == 0xff)
				{
					endtime = CDTXMania.DTX.listChip[index].n発声時刻ms;
					break;
				}
			}
			for (int index = 0; index < CDTXMania.DTX.listChip.Count; index++)
			{
				if (CDTXMania.DTX.listChip[index].nチャンネル番号 == 0x01)
				{
					bgmlength = CDTXMania.DTX.listChip[index].GetDuration() + CDTXMania.DTX.listChip[index].n発声時刻ms;
					break;
				}
			}

			length = Math.Max(endtime, bgmlength);

			gogoXList = new List<int>();
			JumpPointList = new List<STJUMPP>();

			for (int i = 0; i < dTX.listChip.Count; i++)
			{
				CDTX.CChip pChip = dTX.listChip[i];

				if (pChip.n整数値_内部番号 > measureCount && pChip.nチャンネル番号 == 0x50) measureCount = pChip.n整数値_内部番号;

				if (pChip.nチャンネル番号 == 0x9E && !bIsInGoGo)
				{
					bIsInGoGo = true;

					var current = ((double)(pChip.db発声時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0)));
					var width = 0;
					if (CDTXMania.Tx.Tokkun_ProgressBar != null) width = CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Width;

					this.gogoXList.Add((int)(width * (current / length)));
				}
				if (pChip.nチャンネル番号 == 0x9F && bIsInGoGo)
				{
					bIsInGoGo = false;
				}
			}

			this.n小節の総数 = measureCount;
		}

		public override void On非活性化()
		{
			length = 1;
			gogoXList = null;
			JumpPointList = null;
			base.On非活性化();
		}

		public override void OnManagedリソースの作成()
		{
			if (!base.b活性化してない)
			{
				if (CDTXMania.Tx.Tokkun_Background_Up != null) this.ct背景スクロールタイマー = new CCounter(1, CDTXMania.Tx.Tokkun_Background_Up.szテクスチャサイズ.Width, 16, CDTXMania.Timer);
				base.OnManagedリソースの作成();
			}
		}

		public override void OnManagedリソースの解放()
		{
			if (!base.b活性化してない)
			{
				this.ctスクロールカウンター = null;
				this.ct背景スクロールタイマー = null;
				base.OnManagedリソースの解放();
			}
		}

		public override int On進行描画()
		{
			if (!base.b活性化してない)
			{
				if (base.b初めての進行描画)
				{
					base.b初めての進行描画 = false;
				}

				CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, "TRAINING MODE (BETA)");

				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Space) ||CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.RRed2P))
				{
					if (this.b特訓PAUSE)
					{
						CDTXMania.Skin.sound特訓再生音.t再生する();
						this.t演奏を再開する();
					}
					else
					{
						CDTXMania.Skin.sound特訓停止音.t再生する();
						this.t演奏を停止する();
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.LeftArrow) ||CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.LBlue))
				{
					if (this.b特訓PAUSE)
					{
						if (this.n現在の小節線 > 1)
						{
							this.n現在の小節線--;
							CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

							this.t譜面の表示位置を合わせる(true);
							CDTXMania.Skin.sound特訓スクロール音.t再生する();
						}
						if (t配列の値interval以下か(ref this.LBlue, CSound管理.rc演奏用タイマ.nシステム時刻ms, CDTXMania.ConfigIni.TokkunMashInterval))
						{
							for (int index = this.JumpPointList.Count - 1; index >= 0; index--)
							{
								if (this.JumpPointList[index].Time <= CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0))
								{
									this.n現在の小節線 = this.JumpPointList[index].Measure;
									CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;
									//CDTXMania.Skin.soundSkip.t再生する();
									this.t譜面の表示位置を合わせる(false);
									break;
								}
							}
						}
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.PageDown))
				{
					if (this.b特訓PAUSE)
					{
						this.n現在の小節線 -= CDTXMania.ConfigIni.TokkunSkipMeasures;
						if (this.n現在の小節線 <= 0)
							this.n現在の小節線 = 1;

						CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

						this.t譜面の表示位置を合わせる(true);
						CDTXMania.Skin.sound特訓スクロール音.t再生する();
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.RightArrow) ||	CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.RBlue))
				{
					if (this.b特訓PAUSE)
					{
						if (this.n現在の小節線 < this.n小節の総数)
						{
							this.n現在の小節線++;
							CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

							this.t譜面の表示位置を合わせる(true);
							CDTXMania.Skin.sound特訓スクロール音.t再生する();
						}
						if (t配列の値interval以下か(ref this.RBlue, CSound管理.rc演奏用タイマ.nシステム時刻ms, CDTXMania.ConfigIni.TokkunMashInterval))
						{
							for (int index = 0; index < this.JumpPointList.Count; index++)
							{
								if (this.JumpPointList[index].Time >= CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0))
								{
									this.n現在の小節線 = this.JumpPointList[index].Measure;
									CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;
									//CDTXMania.Skin.soundSkip.t再生する();
									this.t譜面の表示位置を合わせる(false);
									break;
								}
							}
						}

					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.PageUp))
				{
					if (this.b特訓PAUSE)
					{
						this.n現在の小節線 += CDTXMania.ConfigIni.TokkunSkipMeasures;
						if (this.n現在の小節線 > this.n小節の総数)
							this.n現在の小節線 = this.n小節の総数;

						CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

						this.t譜面の表示位置を合わせる(true);
						CDTXMania.Skin.sound特訓スクロール音.t再生する();
					}
				}
				if (CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.LBlue2P))
				{
					if (this.b特訓PAUSE)
					{
						if (CDTXMania.ConfigIni.n演奏速度 > 6)
						{
							CDTXMania.ConfigIni.n演奏速度 = CDTXMania.ConfigIni.n演奏速度 - 2;
							this.t譜面の表示位置を合わせる(false);
						}
					}
				}
				if (CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.RBlue2P))
				{
					if (this.b特訓PAUSE)
					{
						if (CDTXMania.ConfigIni.n演奏速度 < 399)
						{
							CDTXMania.ConfigIni.n演奏速度 = CDTXMania.ConfigIni.n演奏速度 + 2;
							this.t譜面の表示位置を合わせる(false);
						}
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Home))
				{
					if (this.b特訓PAUSE)
					{
						if (this.n現在の小節線 > 1)
						{
							this.n現在の小節線 = 1;
							CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

							this.t譜面の表示位置を合わせる(true);
							CDTXMania.Skin.sound特訓スクロール音.t再生する();
						}
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.End))
				{
					if (this.b特訓PAUSE)
					{
						if (this.n現在の小節線 < this.n小節の総数)
						{
							this.n現在の小節線 = this.n小節の総数;
							CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;

							this.t譜面の表示位置を合わせる(true);
							CDTXMania.Skin.sound特訓スクロール音.t再生する();
						}
					}
				}
				if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.A))
					this.t現在の位置にジャンプポイントを設定する();

				if (this.bスクロール中)
				{
					CSound管理.rc演奏用タイマ.n現在時刻ms = EasingCircular(this.ctスクロールカウンター.n現在の値, (int)this.nスクロール前ms, (int)this.nスクロール後ms - (int)this.nスクロール前ms, this.ctスクロールカウンター.n終了値);

					this.ctスクロールカウンター.t進行();

					if ((int)CSound管理.rc演奏用タイマ.n現在時刻ms == (int)this.nスクロール後ms)
					{
						this.bスクロール中 = false;
						CSound管理.rc演奏用タイマ.n現在時刻ms = this.nスクロール後ms;
					}
				}
				if (!this.b特訓PAUSE)
				{
					if (this.n現在の小節線 < CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0])
					{
						this.n現在の小節線 = CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0];
					}

					if (CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0) > this.n最終演奏位置ms)
					{
						this.n最終演奏位置ms = (long)(CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
					}
				}

			}

			var current = (double)(CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
			var percentage = current / length;

			var currentWhite = (double)(this.n最終演奏位置ms);
			var percentageWhite = currentWhite / length;

			if (CDTXMania.Tx.Tokkun_ProgressBarWhite != null) CDTXMania.Tx.Tokkun_ProgressBarWhite.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_ProgressBar_XY[0], CDTXMania.Skin.Game_Training_ProgressBar_XY[1], new Rectangle(1, 1, (int)(CDTXMania.Tx.Tokkun_ProgressBarWhite.szテクスチャサイズ.Width * percentageWhite), CDTXMania.Tx.Tokkun_ProgressBarWhite.szテクスチャサイズ.Height));
			if (CDTXMania.Tx.Tokkun_ProgressBar != null) CDTXMania.Tx.Tokkun_ProgressBar.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_ProgressBar_XY[0], CDTXMania.Skin.Game_Training_ProgressBar_XY[1], new Rectangle(1, 1, (int)(CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Width * percentage), CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Height));
			if (CDTXMania.Tx.Tokkun_GoGoPoint != null)
			{
				foreach (int xpos in gogoXList)
				{
					CDTXMania.Tx.Tokkun_GoGoPoint.t2D描画(CDTXMania.app.Device, xpos + CDTXMania.Skin.Game_Training_ProgressBar_XY[0] - (CDTXMania.Tx.Tokkun_GoGoPoint.szテクスチャサイズ.Width / 2), CDTXMania.Skin.Game_Training_GoGoPoint_Y);
				}
			}

			if (CDTXMania.Tx.Tokkun_JumpPoint != null)
			{
				foreach (STJUMPP xpos in JumpPointList)
				{
					var width = 0;
					if (CDTXMania.Tx.Tokkun_ProgressBar != null) width = CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Width;

					int x = (int)((double)width * ((double)xpos.Time / (double)length));
					CDTXMania.Tx.Tokkun_JumpPoint.t2D描画(CDTXMania.app.Device, x + CDTXMania.Skin.Game_Training_ProgressBar_XY[0] - (CDTXMania.Tx.Tokkun_JumpPoint.szテクスチャサイズ.Width / 2), CDTXMania.Skin.Game_Training_JumpPoint_Y);
				}
			}

			return base.On進行描画();
		}

		public int On進行描画_背景()
		{
			if (this.ct背景スクロールタイマー != null)
			{
				this.ct背景スクロールタイマー.t進行Loop();

				double TexSize = 1280 / CDTXMania.Tx.Tokkun_Background_Up.szテクスチャサイズ.Width;
				// 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
				int ForLoop = (int)Math.Ceiling(TexSize) + 1;
				CDTXMania.Tx.Tokkun_Background_Up.t2D描画(CDTXMania.app.Device, 0 - this.ct背景スクロールタイマー.n現在の値, CDTXMania.Skin.Background_Scroll_Y[0]);
				for (int l = 1; l < ForLoop + 1; l++)
				{
					CDTXMania.Tx.Tokkun_Background_Up.t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Tokkun_Background_Up.szテクスチャサイズ.Width) - this.ct背景スクロールタイマー.n現在の値, CDTXMania.Skin.Background_Scroll_Y[0]);
				}
			}

			if (CDTXMania.Tx.Tokkun_DownBG != null) CDTXMania.Tx.Tokkun_DownBG.t2D描画(CDTXMania.app.Device, 0, 360);
			if (CDTXMania.Tx.Tokkun_BigTaiko != null) CDTXMania.Tx.Tokkun_BigTaiko.t2D描画(CDTXMania.app.Device, 334, 400);

			return base.On進行描画();
		}

		public void On進行描画_小節_速度()
		{
			if (CDTXMania.Tx.Tokkun_Speed_Measure != null)
				CDTXMania.Tx.Tokkun_Speed_Measure.t2D描画(CDTXMania.app.Device, 0, 360);
			var maxMeasureStr = this.n小節の総数.ToString();
			var measureStr = CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0].ToString();
			if (CDTXMania.Tx.Tokkun_SmallNumber != null)
			{
				var x = CDTXMania.Skin.Game_Training_MaxMeasureCount_XY[0];
				foreach (char c in maxMeasureStr)
				{
					var currentNum = int.Parse(c.ToString());
					CDTXMania.Tx.Tokkun_SmallNumber.t2D描画(CDTXMania.app.Device, x, CDTXMania.Skin.Game_Training_MaxMeasureCount_XY[1], new Rectangle(CDTXMania.Skin.Game_Training_SmallNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_SmallNumber_Width, CDTXMania.Tx.Tokkun_SmallNumber.szテクスチャサイズ.Height));
					x += CDTXMania.Skin.Game_Training_SmallNumber_Width - 2;
				}
			}

			var subtractVal = (CDTXMania.Skin.Game_Training_BigNumber_Width - 2) * (measureStr.Length - 1);

			if (CDTXMania.Tx.Tokkun_BigNumber != null)
			{
				var x = CDTXMania.Skin.Game_Training_CurrentMeasureCount_XY[0];
				foreach (char c in measureStr)
				{
					var currentNum = int.Parse(c.ToString());
					CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, x - subtractVal, CDTXMania.Skin.Game_Training_CurrentMeasureCount_XY[1], new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));
					x += CDTXMania.Skin.Game_Training_BigNumber_Width - 2;
				}

				var PlaySpdtmp = CDTXMania.ConfigIni.n演奏速度 / 20.0d * 10.0d;
				PlaySpdtmp = Math.Round(PlaySpdtmp, MidpointRounding.AwayFromZero);

				var playSpd = PlaySpdtmp / 10.0d;
				var playSpdI = playSpd - (int)playSpd;
				var playSpdStr = Decimal.Round((decimal)playSpdI, 1, MidpointRounding.AwayFromZero).ToString();
				var decimalStr = (playSpdStr == "0") ? "0" : playSpdStr[2].ToString();

				CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_SpeedDisplay_XY[0], CDTXMania.Skin.Game_Training_SpeedDisplay_XY[1], new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * int.Parse(decimalStr), 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));

				x = CDTXMania.Skin.Game_Training_SpeedDisplay_XY[0] - 25;

				subtractVal = CDTXMania.Skin.Game_Training_BigNumber_Width * (((int)playSpd).ToString().Length - 1);

				foreach (char c in ((int)playSpd).ToString())
				{
					var currentNum = int.Parse(c.ToString());
					CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, x - subtractVal, CDTXMania.Skin.Game_Training_SpeedDisplay_XY[1], new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));
					x += CDTXMania.Skin.Game_Training_BigNumber_Width - 2;
				}
			}
		}

		public void t演奏を停止する()
		{
			CDTX dTX = CDTXMania.DTX;

			this.nスクロール後ms = CSound管理.rc演奏用タイマ.n現在時刻ms;

			CDTXMania.stage演奏ドラム画面.On活性化();
			CSound管理.rc演奏用タイマ.t一時停止();

			for (int i = 0; i < dTX.listChip.Count; i++)
			{
				CDTX.CChip pChip = dTX.listChip[i];
				pChip.bHit = false;
				if (dTX.listChip[i].nチャンネル番号 != 0x50)
				{
					pChip.bShow = true;
					pChip.b可視 = true;
				}
			}

			CDTXMania.DTX.t全チップの再生一時停止();
			CDTXMania.stage演奏ドラム画面.bPAUSE = true;
			CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = this.n現在の小節線;
			this.b特訓PAUSE = true;

			this.t譜面の表示位置を合わせる(false);
		}

		public void t演奏を再開する()
		{
			CDTX dTX = CDTXMania.DTX;

			this.bスクロール中 = false;
			CSound管理.rc演奏用タイマ.n現在時刻ms = this.nスクロール後ms;

			int n演奏開始Chip = CDTXMania.stage演奏ドラム画面.n現在のトップChip;
			int finalStartBar;

			finalStartBar = this.n現在の小節線 - 2;
			if (finalStartBar < 0) finalStartBar = 0;

			CDTXMania.stage演奏ドラム画面.t演奏位置の変更(finalStartBar, 0);


			int n少し戻ってから演奏開始Chip = CDTXMania.stage演奏ドラム画面.n現在のトップChip;

			CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] = 0;
			CDTXMania.stage演奏ドラム画面.t数値の初期化(true, true);
			CDTXMania.stage演奏ドラム画面.On活性化();

			for (int i = 0; i < dTX.listChip.Count; i++)
			{
				if (i < n演奏開始Chip && (dTX.listChip[i].nチャンネル番号 > 0x10 && dTX.listChip[i].nチャンネル番号 < 0x20)) //2020.07.08 ノーツだけ消す。 null参照回避のために順番変更
				{
					dTX.listChip[i].bHit = true;
					dTX.listChip[i].IsHitted = true;
					dTX.listChip[i].b可視 = false;
					dTX.listChip[i].bShow = false;
				}
				if (i < n少し戻ってから演奏開始Chip && dTX.listChip[i].nチャンネル番号 == 0x01)
				{
					dTX.listChip[i].bHit = true;
					dTX.listChip[i].IsHitted = true;
					dTX.listChip[i].b可視 = false;
					dTX.listChip[i].bShow = false;
				}
				if (dTX.listChip[i].nチャンネル番号 == 0x50 && dTX.listChip[i].n整数値_内部番号 < finalStartBar)
				{
					dTX.listChip[i].bHit = true;
					dTX.listChip[i].IsHitted = true;
				}

			}

			for (int i = 0; i < CDTXMania.ConfigIni.nPlayerCount; i++)
			{
				CDTXMania.stage演奏ドラム画面.chip現在処理中の連打チップ[i] = null;
			}

			this.b特訓PAUSE = false;
		}

		public void t譜面の表示位置を合わせる(bool doScroll)
		{
			this.nスクロール前ms = CSound管理.rc演奏用タイマ.n現在時刻ms;

			CDTX dTX = CDTXMania.DTX;

			bool bSuccessSeek = false;
			for (int i = 0; i < dTX.listChip.Count; i++)
			{
				CDTX.CChip pChip = dTX.listChip[i];

				if (pChip.nチャンネル番号 == 0x50 && pChip.n整数値_内部番号 > n現在の小節線 - 1)
				{
					bSuccessSeek = true;
					CDTXMania.stage演奏ドラム画面.n現在のトップChip = i;
					break;
				}
			}
			if (!bSuccessSeek)
			{
				CDTXMania.stage演奏ドラム画面.n現在のトップChip = 0;
			}
			else
			{
				while (dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip].n発声時刻ms == dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip - 1].n発声時刻ms && CDTXMania.stage演奏ドラム画面.n現在のトップChip != 0)
					CDTXMania.stage演奏ドラム画面.n現在のトップChip--;
			}

			if (doScroll)
			{
				this.nスクロール後ms = (long)(dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip].n発声時刻ms / (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
				this.bスクロール中 = true;

				this.ctスクロールカウンター = new CCounter(0, CDTXMania.Skin.Game_Training_ScrollTime, 1, CDTXMania.Timer);
			}
			else
			{
				CSound管理.rc演奏用タイマ.n現在時刻ms = (long)(dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip].n発声時刻ms / (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
				this.nスクロール後ms = CSound管理.rc演奏用タイマ.n現在時刻ms;
			}
		}

		public void t現在の位置にジャンプポイントを設定する()
		{
			if (!this.bスクロール中 && this.b特訓PAUSE)
			{
				if (!JumpPointList.Contains(new STJUMPP() { Time = (long)(CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0)), Measure = this.n現在の小節線 }))
					JumpPointList.Add(new STJUMPP() { Time = (long)(CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0)), Measure = this.n現在の小節線 });
				JumpPointList.Sort((a, b) => a.Time.CompareTo(b.Time));
			}
		}

		private bool t配列の値interval以下か(ref long[] array, long num, int interval)
		{
			long[] arraytmp = array;
			for (int index = 0; index < (array.Length - 1); index++)
			{
				array[index] = array[index + 1];
			}
			array[array.Length - 1] = num;
			return Math.Abs(num - arraytmp[0]) <= interval;
		}

		public int n現在の小節線;
		public int n小節の総数;

		#region [private]
		private long nスクロール前ms;
		private long nスクロール後ms;
		private long n最終演奏位置ms;

		private bool b特訓PAUSE;
		private bool bスクロール中;

		private CCounter ctスクロールカウンター;
		private CCounter ct背景スクロールタイマー;
		private long length = 1;

		private List<int> gogoXList;
		private List<STJUMPP> JumpPointList;
		private long[] LBlue = new long[] { 0, 0, 0, 0, 0 };
		private long[] RBlue = new long[] { 0, 0, 0, 0, 0 };

		private struct STJUMPP
		{
			public long Time;
			public int Measure;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time">今の時間</param>
		/// <param name="begin">最初の値</param>
		/// <param name="change">最終の値-最初の値</param>
		/// <param name="duration">全体の時間</param>
		/// <returns></returns>
		private int EasingCircular(int time, int begin, int change, int duration)
		{
			double t = time, b = begin, c = change, d = duration;

			t = t / d * 2;
			if (t < 1)
				return (int)(-c / 2 * (Math.Sqrt(1 - t * t) - 1) + b);
			else
			{
				t = t - 2;
				return (int)(c / 2 * (Math.Sqrt(1 - t * t) + 1) + b);
			}
		}

		#endregion
	}
}