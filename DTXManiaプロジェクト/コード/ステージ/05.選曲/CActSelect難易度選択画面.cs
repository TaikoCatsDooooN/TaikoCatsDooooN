using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Text;

using SlimDX;
using FDK;

namespace DTXMania
{
    /// <summary>
    /// 難易度選択画面。
    /// </summary>
	internal class CActSelect難易度選択画面 : CActivity
    {
        // プロパティ

        public bool bスクロール中
        {
            get
            {
                if (this.n目標のスクロールカウンタ == 0)
                {
                    return (this.n現在のスクロールカウンタ != 0);
                }
                return true;
            }
        }
        public bool bIsDifficltSelect;

        // コンストラクタ

        public CActSelect難易度選択画面()
        {
            base.b活性化してない = true;
        }


        // メソッド
        public int t指定した方向に近い難易度番号を返す(int nDIRECTION, int pos)
        {
            if (nDIRECTION == 0)
            {
                for (int i = pos; i < 5; i++)
                {
                    if (i == pos) continue;
                    if (CDTXMania.stage選曲.r現在選択中の曲.arスコア[i] != null) return i;
                    if (i == 4) return this.t指定した方向に近い難易度番号を返す(0, 0);
                }
            }
            else
            {
                for (int i = pos; i > -1; i--)
                {
                    if (pos == i) continue;
                    if (CDTXMania.stage選曲.r現在選択中の曲.arスコア[i] != null) return i;
                    if (i == 0) return this.t指定した方向に近い難易度番号を返す(1, 4);
                }
            }
            return pos;
        }

        public void t次に移動()
        {
            if (this.n現在の選択行 < 4)
            {
                if (!b裏譜面)
                {
                    this.n現在の選択行 += 1;
                }
                else
                {
                    if (this.n現在の選択行 == 3)
                    {
                        this.n現在の選択行 += 2;
                    }
                    else
                    {
                        this.n現在の選択行 += 1;
                    }
                }
            }
            else if (this.n現在の選択行 >= 4)
            {
                if (this.nスイッチカウント < 9 && CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[4] >= 0)
                {
                    this.nスイッチカウント += 1;
                }
                else if (this.nスイッチカウント == 9)
                {
                    ct裏譜面へ.n現在の値 = 0;
                    ct表譜面へ.n現在の値 = 0;
                    if (!b裏譜面 && n現在の選択行 == 4)
                    {
                        this.n現在の選択行 = 5;
                    }
                    else if (b裏譜面 && n現在の選択行 == 5)
                    {
                        this.n現在の選択行 = 4;
                    }
                    C共通.bToggleBoolian(ref this.b裏譜面);
                    this.b表裏アニメーション中 = true;
                    this.nスイッチカウント = 0;
                    if (this.sound裏切り替え音 != null)
                    {
                        this.sound裏切り替え音.t再生を開始する();
                    }
                }
            }

            this.ct移動 = new CCounter(-100, 0, 1, CDTXMania.Timer);
        }
        public void t前に移動()
        {
            if (this.n現在の選択行 > 0)
            {
                if (!b裏譜面)
                {
                    this.n現在の選択行 -= 1;
                }
                else
                {
                    if (this.n現在の選択行 == 5)
                    {
                        this.n現在の選択行 -= 2;
                    }
                    else
                    {
                        this.n現在の選択行 -= 1;
                    }
                }
                this.nスイッチカウント = 0;
            }

            this.ct移動 = new CCounter(100, 0, 1, CDTXMania.Timer);
        }
        public void t選択画面初期化()
        {
            this.b初めての進行描画 = true;
        }

        // CActivity 実装

        public override void On活性化()
        {
            if (this.b活性化してる)
                return;

            this.b登場アニメ全部完了 = false;
            this.n目標のスクロールカウンタ = 0;
            this.n現在のスクロールカウンタ = 0;
            this.nスイッチカウント = 0;

            this.ct移動 = new CCounter();

            base.On活性化();
        }
        public override void On非活性化()
        {
            if (this.b活性化してない)
                return;

            for (int i = 0; i < 13; i++)
                this.ct登場アニメ用[i] = null;

            this.ct移動 = null;

            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (this.b活性化してない)
                return;

            if (CDTXMania.Tx.SongSelect_Difficulty_Bar != null)
                ctカーソル点滅アニメ = new CCounter(0, 384, 2, CDTXMania.Timer);
            if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Back != null)
                ctミニカーソル点滅アニメ = new CCounter(0, 384, 2, CDTXMania.Timer);
            if (CDTXMania.Tx.SongSelect_Difficulty_Branch != null)
                ct譜面分岐 = new CCounter(1, 200, 10, CDTXMania.Timer);
            if (CDTXMania.Tx.SongSelect_Difficulty_Select_Switch != null)
            {
                ct裏譜面へ = new CCounter(0, 992, 1, CDTXMania.Timer);
                ct表譜面へ = new CCounter(0, 992, 1, CDTXMania.Timer);
            }

            this.sound難しさを選ぶ = CDTXMania.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\SongSelect\Difficulty_Select.ogg"), ESoundGroup.SoundEffect);
            this.sound裏切り替え音 = CDTXMania.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\SongSelect\Edit_Switch.ogg"), ESoundGroup.SoundEffect);
            base.OnManagedリソースの作成();
        }
        public override void OnManagedリソースの解放()
        {
            if (this.b活性化してない)
                return;

            base.OnManagedリソースの解放();
        }
        public override int On進行描画()
        {
            if (this.b活性化してない)
                return 0;

            #region [ 初めての進行描画 ]
            //-----------------
            if (this.b初めての進行描画)
            {
                for (int i = 0; i < 13; i++)
                    this.ct登場アニメ用[i] = new CCounter(-i * 10, 100, 3, CDTXMania.Timer);
                CDTXMania.stage選曲.t選択曲変更通知();

                if (CDTXMania.stage選曲.act曲リスト.n現在選択中の曲の現在の難易度レベル == 4)
                {
                    this.n現在の選択行 = 5;
                    this.b裏譜面 = true;
                }
                else
                {
                    this.n現在の選択行 = 1 + CDTXMania.stage選曲.act曲リスト.n現在選択中の曲の現在の難易度レベル;
                    this.b裏譜面 = false;
                }
                    
                if (this.sound難しさを選ぶ != null)
                {
                    this.sound難しさを選ぶ.t再生を開始する();
                }

                if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[4] < 0)
                {
                    b裏譜面 = false;
                }

                this.b選曲後 = false;
                base.b初めての進行描画 = false;
            }

            #endregion

            {
                #region [ (2) 通常フェーズの進行。]

                if (!this.b選曲後)
                {
                    if (CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.RBlue) || CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.RightArrow))
                    {
                        CDTXMania.Skin.soundカーソル移動音.t再生する();
                        this.t次に移動();
                    }
                    else if (CDTXMania.Pad.b押された(E楽器パート.DRUMS, Eパッド.LBlue) || CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.LeftArrow))
                    {
                        CDTXMania.Skin.soundカーソル移動音.t再生する();
                        this.t前に移動();
                    }
                    else if ((CDTXMania.Pad.b押されたDGB(Eパッド.Decide) || (CDTXMania.Pad.b押されたDGB(Eパッド.LRed) || CDTXMania.Pad.b押されたDGB(Eパッド.RRed)) ||
                            ((CDTXMania.ConfigIni.bEnterがキー割り当てのどこにも使用されていない && CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Return)))))
                    {
                        if (this.n現在の選択行 == 0)
                            //もどる
                            CDTXMania.stage選曲.t難易度選択画面を閉じる();
                        else
                        {
                            //かんたん・ふつう・むずかしい・おに・エディット
                            {
                                //if (this.sound難しさを選ぶ.b再生中) this.sound難しさを選ぶ.t再生を停止する();
                                //CDTXMania.Skin.sound決定音.t再生する();
                                if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[this.n現在の選択行 - 1] >= 0)
                                {
                                    CDTXMania.Skin.sound曲決定音.t再生する();
                                    this.b選曲後 = true;
                                    CDTXMania.stage選曲.t曲を選択する(this.n現在の選択行 - 1);
                                }
                            }
                        }
                    }
                    else if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Escape))
                    {
                        CDTXMania.stage選曲.t難易度選択画面を閉じる();
                    }
                    else if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Tab))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[i] >= 0)
                            {
                                if (this.sound裏切り替え音 != null)
                                {
                                    this.sound裏切り替え音.t再生を開始する();
                                }
                                ct裏譜面へ.n現在の値 = 0;
                                ct表譜面へ.n現在の値 = 0;
                                if (!b裏譜面 && n現在の選択行 == 4)
                                {
                                    this.n現在の選択行 = 5;
                                }
                                else if (b裏譜面 && n現在の選択行 == 5)
                                {
                                    this.n現在の選択行 = 4;
                                }
                                C共通.bToggleBoolian(ref this.b裏譜面);
                                this.b表裏アニメーション中 = true;
                            }
                        }
                    }
                }

                CDTXMania.Tx.SongSelect_Difficulty_Select_Back.t2D描画(CDTXMania.app.Device, 319, 114);

                for (int i = 0; i < 5; i++)
                {
                    if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[i] >= 0)
                    {
                        // レベルが0以上
                        CDTXMania.Tx.SongSelect_Difficulty_Select[i].color4 = new Color4(1f, 1f, 1f);
                        if (i == 3 && b裏譜面) ;
                        else if (i == 4 && b裏譜面)
                        {
                            // エディット
                            CDTXMania.Tx.SongSelect_Difficulty_Select[i].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                        }
                        else if (i != 4)
                        {
                            CDTXMania.Tx.SongSelect_Difficulty_Select[i].t2D描画(CDTXMania.app.Device, 450 + (100 * i), 84);
                        }
                    }
                    else
                    {
                        // レベルが0未満 = 譜面がないとみなす
                        CDTXMania.Tx.SongSelect_Difficulty_Select[i].color4 = new Color4(0.5f, 0.5f, 0.5f);
                        if (i == 3 && b裏譜面) ;
                        else if (i == 4 && b裏譜面)
                        {
                            // エディット
                            CDTXMania.Tx.SongSelect_Difficulty_Select[i].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                        }
                        else if (i != 4)
                        {
                            CDTXMania.Tx.SongSelect_Difficulty_Select[i].t2D描画(CDTXMania.app.Device, 450 + (100 * i), 84);
                        }
                    }

                    #region[ 星 ]
                    if (CDTXMania.Tx.SongSelect_Difficulty_Branch != null)
                        ct譜面分岐.t進行Loop();
                    if (CDTXMania.Tx.SongSelect_Level != null)
                    {
                        // 全難易度表示
                        for (int n = 0; n < CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.nレベル[i]; n++)
                        {
                            // 星11以上はループ終了
                            //if (n > 9) break;
                            // 裏なら鬼と同じ場所に
                            if (i == 3 && b裏譜面) break;
                            if (i == 4 && b裏譜面)
                            {
                                if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.b譜面分岐[i] && CDTXMania.Tx.SongSelect_Difficulty_Branch != null && this.ct譜面分岐.n現在の値 >= 0 && this.ct譜面分岐.n現在の値 < 100)
                                    CDTXMania.Tx.SongSelect_Difficulty_Branch.t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                                else
                                    CDTXMania.Tx.SongSelect_Difficulty_Star_Edit.t2D描画(CDTXMania.app.Device, 479 + (100 * 3), 474 - (n * 197/10));
                            }
                            if (i != 4)
                            {
                                if (CDTXMania.stage選曲.r現在選択中のスコア.譜面情報.b譜面分岐[i] && CDTXMania.Tx.SongSelect_Difficulty_Branch != null && this.ct譜面分岐.n現在の値 >= 0 && this.ct譜面分岐.n現在の値 < 100)
                                    CDTXMania.Tx.SongSelect_Difficulty_Branch.t2D描画(CDTXMania.app.Device, 450 + (100 * i), 84);
                                else
                                    CDTXMania.Tx.SongSelect_Difficulty_Star.t2D描画(CDTXMania.app.Device, 479 + (100 * i), 474 - (n * 197/10));
                            }
                        }
                    }
                    #endregion
                }

                if (CDTXMania.Tx.SongSelect_Difficulty_Select_Switch != null)
                {
                    this.ct裏譜面へ.t進行();
                    this.ct表譜面へ.t進行();
                    if (this.b表裏アニメーション中)
                    {
                        if (b裏譜面)
                        {
                            if (this.ct裏譜面へ.n現在の値 <= 120)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[0].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct裏譜面へ.n現在の値 <= 240)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[1].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct裏譜面へ.n現在の値 <= 360)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[2].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[3].n透明度 = 255;
                            }
                            else if (this.ct裏譜面へ.n現在の値 <= 480)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[3].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct裏譜面へ.n現在の値 > 480)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[3].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[3].n透明度 = 255 - ((this.ct裏譜面へ.n現在の値 - 480) / 2);
                            }
                            else if (this.ct裏譜面へ.n現在の値 >= 992)
                            {
                                b表裏アニメーション中 = false;
                            }
                        }

                        else if (!b裏譜面)
                        {
                            if (this.ct表譜面へ.n現在の値 <= 120)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[4].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct表譜面へ.n現在の値 <= 240)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[5].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct表譜面へ.n現在の値 <= 360)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[6].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[7].n透明度 = 255;
                            }
                            else if (this.ct表譜面へ.n現在の値 <= 480)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[7].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                            }
                            else if (this.ct表譜面へ.n現在の値 > 480)
                            {
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[7].t2D描画(CDTXMania.app.Device, 450 + (100 * 3), 84);
                                CDTXMania.Tx.SongSelect_Difficulty_Select_Switch[7].n透明度 = 255 - ((this.ct表譜面へ.n現在の値 - 480) / 2);
                            }
                            else if (this.ct表譜面へ.n現在の値 >= 992)
                            {
                                b表裏アニメーション中 = false;
                            }
                        }
                    }
                }

                #region[ カーソル ]

                if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect != null)
                    ctカーソル点滅アニメ.t進行Loop();
                if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect != null)
                    ctミニカーソル点滅アニメ.t進行Loop();

                if (this.n現在の選択行 == 5)
                {
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar != null)
                        CDTXMania.Tx.SongSelect_Difficulty_Bar.t2D描画(CDTXMania.app.Device, 450 + (3 * 100), -6);
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect != null)
                    {
                        if (ctカーソル点滅アニメ.n現在の値 < 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = ctカーソル点滅アニメ.n現在の値;
                        if (ctカーソル点滅アニメ.n現在の値 >= 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = 127;
                        if (ctカーソル点滅アニメ.n現在の値 >= 255)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = 384 - ctカーソル点滅アニメ.n現在の値;
                        CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.t2D描画(CDTXMania.app.Device, 450 + (3 * 100), -6);
                    }
                }
                else if (this.n現在の選択行 == 0)
                {
                    //case E項目種類.オプション:
                    //case E項目種類.音色:
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Back != null)
                        CDTXMania.Tx.SongSelect_Difficulty_Bar_Back.t2D描画(CDTXMania.app.Device, 301, -41);
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect != null)
                    {
                        if (ctカーソル点滅アニメ.n現在の値 < 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect.n透明度 = ctカーソル点滅アニメ.n現在の値;
                        if (ctカーソル点滅アニメ.n現在の値 >= 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect.n透明度 = 127;
                        if (ctカーソル点滅アニメ.n現在の値 >= 255)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect.n透明度 = 384 - ctカーソル点滅アニメ.n現在の値;
                        CDTXMania.Tx.SongSelect_Difficulty_Bar_Back_Effect.t2D描画(CDTXMania.app.Device, 301, -41);
                    }
                }
                else
                {
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar != null)
                        CDTXMania.Tx.SongSelect_Difficulty_Bar.t2D描画(CDTXMania.app.Device, 450 + (this.n現在の選択行 - 1) * 100, -6);
                    if (CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect != null)
                    {
                        if (ctカーソル点滅アニメ.n現在の値 < 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = ctカーソル点滅アニメ.n現在の値;
                        if (ctカーソル点滅アニメ.n現在の値 >= 127)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = 127;
                        if (ctカーソル点滅アニメ.n現在の値 >= 255)
                            CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.n透明度 = 384 - ctカーソル点滅アニメ.n現在の値;
                        CDTXMania.Tx.SongSelect_Difficulty_Bar_Effect.t2D描画(CDTXMania.app.Device, 450 + (this.n現在の選択行 - 1) * 100, -6);
                    }
                }
                #endregion
                //-----------------
                #endregion
            }

            return 0;
        }


        // その他

        #region [ private ]
        //-----------------

        public CSound sound難しさを選ぶ;
        public CSound sound裏切り替え音;

        private bool b登場アニメ全部完了;
        private bool b選曲後;
        private bool b裏譜面;
        private bool b表裏アニメーション中;

        private CCounter[] ct登場アニメ用 = new CCounter[13];
        private CCounter ct移動;
        private CCounter ctカーソル点滅アニメ;
        private CCounter ctミニカーソル点滅アニメ;
        private CCounter ct譜面分岐;
        private CCounter ct裏譜面へ;
        private CCounter ct表譜面へ;

        private int n現在のスクロールカウンタ;
        private int n現在の選択行;
        private int n目標のスクロールカウンタ;
        private int nスイッチカウント;

        //-----------------

        #endregion
    }
}
