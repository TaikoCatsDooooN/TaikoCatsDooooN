using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SlimDX.DirectInput;
using FDK;
using System.Diagnostics;

namespace DTXMania
{
    internal class CAct特訓モード : CActivity
    {
        public CAct特訓モード()
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
            var length = (CDTXMania.DTX.listChip.Count > 0) ? CDTXMania.DTX.listChip[CDTXMania.DTX.listChip.Count - 1].n発声時刻ms : 0;

            for (int i = 0; i < dTX.listChip.Count; i++)
            {
                CDTX.CChip pChip = dTX.listChip[i];

                if (pChip.n整数値_内部番号 > measureCount) measureCount = pChip.n整数値_内部番号;

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

                if (CDTXMania.Input管理.Keyboard.bキーが押された((int)Key.Space))
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
                if (CDTXMania.Input管理.Keyboard.bキーが押された((int)Key.LeftArrow) || CDTXMania.Pad.b押されたDGB(Eパッド.LBlue))
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
                    }
                }
                if (CDTXMania.Input管理.Keyboard.bキーが押された((int)Key.RightArrow) || CDTXMania.Pad.b押されたDGB(Eパッド.RBlue))
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
                    }
                }

                if (this.bスクロール中)
                {
                    CSound管理.rc演奏用タイマ.n現在時刻ms = easing.EaseOut(this.ctスクロールカウンター, (int)this.nスクロール前ms, (int)this.nスクロール後ms, Easing.CalcType.Circular);

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

                    if (CSound管理.rc演奏用タイマ.n現在時刻ms > this.n最終演奏位置ms)
                    {
                        this.n最終演奏位置ms = CSound管理.rc演奏用タイマ.n現在時刻ms;
                    }
                }
            }
            return base.On進行描画();
        }

        public int On進行描画_背景()
        {
            if (CDTXMania.Tx.Tokkun_DownBG != null) CDTXMania.Tx.Tokkun_DownBG.t2D描画(CDTXMania.app.Device, 0, 360);
            if (CDTXMania.Tx.Tokkun_BigTaiko != null) CDTXMania.Tx.Tokkun_BigTaiko.t2D描画(CDTXMania.app.Device, 334, 400);

            var length = (CDTXMania.DTX.listChip.Count > 0) ? CDTXMania.DTX.listChip[CDTXMania.DTX.listChip.Count - 1].n発声時刻ms : 0;

            var current = (double)(CSound管理.rc演奏用タイマ.n現在時刻ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
            var percentage = current / length;

            var currentWhite = (double)(this.n最終演奏位置ms * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
            var percentageWhite = currentWhite / length;

            if (CDTXMania.Tx.Tokkun_ProgressBarWhite != null) CDTXMania.Tx.Tokkun_ProgressBarWhite.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_ProgressBar_X, CDTXMania.Skin.Game_Training_ProgressBar_Y, new Rectangle(1, 1, (int)(CDTXMania.Tx.Tokkun_ProgressBarWhite.szテクスチャサイズ.Width * percentageWhite), CDTXMania.Tx.Tokkun_ProgressBarWhite.szテクスチャサイズ.Height));
            if (CDTXMania.Tx.Tokkun_ProgressBar != null) CDTXMania.Tx.Tokkun_ProgressBar.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_ProgressBar_X, CDTXMania.Skin.Game_Training_ProgressBar_Y, new Rectangle(1, 1, (int)(CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Width * percentage), CDTXMania.Tx.Tokkun_ProgressBar.szテクスチャサイズ.Height));

            if (CDTXMania.Tx.Tokkun_GoGoPoint != null)
            {
                foreach (int xpos in gogoXList)
                {
                    CDTXMania.Tx.Tokkun_GoGoPoint.t2D描画(CDTXMania.app.Device, xpos + CDTXMania.Skin.Game_Training_ProgressBar_X - (CDTXMania.Tx.Tokkun_GoGoPoint.szテクスチャサイズ.Width / 2), CDTXMania.Skin.Game_Training_GoGoPoint_Y);
                }
            }

            if (this.ct背景スクロールタイマー != null)
            {
                this.ct背景スクロールタイマー.t進行Loop();

                double TexSize = 1280 / CDTXMania.Tx.Tokkun_Background_Up.szテクスチャサイズ.Width;
                int ForLoop = (int)Math.Ceiling(TexSize) + 1;
                CDTXMania.Tx.Tokkun_Background_Up.t2D描画(CDTXMania.app.Device, 0 - this.ct背景スクロールタイマー.n現在の値, CDTXMania.Skin.Background_Scroll_Y[0]);
                for (int l = 1; l < ForLoop + 1; l++)
                {
                    CDTXMania.Tx.Tokkun_Background_Up.t2D描画(CDTXMania.app.Device, (l * CDTXMania.Tx.Tokkun_Background_Up.szテクスチャサイズ.Width) - this.ct背景スクロールタイマー.n現在の値, CDTXMania.Skin.Background_Scroll_Y[0]);
                }
            }

            var maxMeasureStr = this.n小節の総数.ToString();
            var measureStr = CDTXMania.stage演奏ドラム画面.actPlayInfo.NowMeasure[0].ToString();
            if (CDTXMania.Tx.Tokkun_SmallNumber != null)
            {
                var x = CDTXMania.Skin.Game_Training_MaxMeasureCount_X;
                foreach (char c in maxMeasureStr)
                {
                    var currentNum = int.Parse(c.ToString());
                    CDTXMania.Tx.Tokkun_SmallNumber.t2D描画(CDTXMania.app.Device, x, CDTXMania.Skin.Game_Training_MaxMeasureCount_Y, new Rectangle(CDTXMania.Skin.Game_Training_SmallNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_SmallNumber_Width, CDTXMania.Tx.Tokkun_SmallNumber.szテクスチャサイズ.Height));
                    x += CDTXMania.Skin.Game_Training_SmallNumber_Width - 2;
                }
            }

            var subtractVal = (CDTXMania.Skin.Game_Training_BigNumber_Width - 2) * (measureStr.Length - 1);

            if (CDTXMania.Tx.Tokkun_BigNumber != null)
            {
                var x = CDTXMania.Skin.Game_Training_CurrentMeasureCount_X;
                foreach (char c in measureStr)
                {
                    var currentNum = int.Parse(c.ToString());
                    CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, x - subtractVal, CDTXMania.Skin.Game_Training_CurrentMeasureCount_Y, new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));
                    x += CDTXMania.Skin.Game_Training_BigNumber_Width - 2;
                }

                var playSpd = CDTXMania.ConfigIni.n演奏速度 / 20.0d;
                var playSpdI = playSpd - (int)playSpd;
                var playSpdStr = Decimal.Round((decimal)playSpdI, 1, MidpointRounding.AwayFromZero).ToString();
                var decimalStr = (playSpdStr == "0") ? "0" : playSpdStr[2].ToString();

                CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Training_SpeedDisplay_X, CDTXMania.Skin.Game_Training_SpeedDisplay_Y, new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * int.Parse(decimalStr), 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));

                x = CDTXMania.Skin.Game_Training_SpeedDisplay_X - 25;

                subtractVal = CDTXMania.Skin.Game_Training_BigNumber_Width * (((int)playSpd).ToString().Length - 1);

                foreach (char c in ((int)playSpd).ToString())
                {
                    var currentNum = int.Parse(c.ToString());
                    CDTXMania.Tx.Tokkun_BigNumber.t2D描画(CDTXMania.app.Device, x - subtractVal, CDTXMania.Skin.Game_Training_SpeedDisplay_Y, new Rectangle(CDTXMania.Skin.Game_Training_BigNumber_Width * currentNum, 0, CDTXMania.Skin.Game_Training_BigNumber_Width, CDTXMania.Tx.Tokkun_BigNumber.szテクスチャサイズ.Height));
                    x += CDTXMania.Skin.Game_Training_BigNumber_Width - 2;
                }
            }

            return base.On進行描画();
        }

        public void t演奏を停止する()
        {
            CDTX dTX = CDTXMania.DTX;

            this.nスクロール後ms = CSound管理.rc演奏用タイマ.n現在時刻ms;

            CDTXMania.stage演奏ドラム画面.actAVI.tReset();
            CDTXMania.stage演奏ドラム画面.On活性化();
            CSound管理.rc演奏用タイマ.t一時停止();

            for (int i = 0; i < dTX.listChip.Count; i++)
            {
                CDTX.CChip pChip = dTX.listChip[i];
                pChip.bHit = false;
                pChip.bShow = true;
                pChip.b可視 = true;
            }

            CDTXMania.DTX.t全チップの再生一時停止();
            CDTXMania.stage演奏ドラム画面.bPAUSE = true;
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

            if (this.n現在の小節線 <= 0) finalStartBar = this.n現在の小節線;
            else finalStartBar = this.n現在の小節線 - 1;

            CDTXMania.stage演奏ドラム画面.t演奏位置の変更(finalStartBar, 0);

            for (int i = 0; i < dTX.listChip.Count; i++)
            {
                if (i < n演奏開始Chip)
                {
                    dTX.listChip[i].bHit = true;
                    dTX.listChip[i].IsHitted = true;
                    dTX.listChip[i].b可視 = false;
                    dTX.listChip[i].bShow = false;
                }
            }

            CDTXMania.stage演奏ドラム画面.t数値の初期化(true, true);
            CDTXMania.stage演奏ドラム画面.actAVI.tReset();
            CDTXMania.stage演奏ドラム画面.On活性化();

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

                if (pChip.n発声位置 < 384 * (n現在の小節線))
                {
                    continue;
                }
                else
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

            if (doScroll)
            {
                this.nスクロール後ms = dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip].n発声時刻ms;
                this.bスクロール中 = true;

                this.ctスクロールカウンター = new CCounter(0, CDTXMania.Skin.Game_Training_ScrollTime, 1, CDTXMania.Timer);
            }
            else
            {
                CSound管理.rc演奏用タイマ.n現在時刻ms = dTX.listChip[CDTXMania.stage演奏ドラム画面.n現在のトップChip].n発声時刻ms;
            }
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
        private Easing easing = new Easing();

        private List<int> gogoXList = new List<int>();
        #endregion
    }
}
