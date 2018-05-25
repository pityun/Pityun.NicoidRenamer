using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Pityun.NicoidRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = null; // 変数の宣言
            while (true) // 無限ループ
            {
                Console.WriteLine("リネームしたいファイルの有るフォルダのパスを入力してください。");
                directory = Console.ReadLine(); // エンターキーが押されるまでに入力されt文字列を受け取る

                if (Directory.Exists(directory)) // ディレクトリが存在”する”時
                {
                    break; // ループから抜け出す
                }
                else // 存在しない時
                {
                    Console.WriteLine("知らない場所ですね。");
                }
            }

            Delete(directory);

            

            string[] jfiles = Directory.GetFiles(directory, "*.json"); // 拡張子がjsonのファイル名のリストを取得（配列）
             
                foreach (string j in jfiles) // 配列のファイル名を一つずつ処理
                {
                    RenameMovieFile(j);
                    Console.WriteLine(j); // 処理対象のjsonファイルの名前を出力

                }

                Console.WriteLine("終了しました。閉じるにはEnterキーを押してください");
                Console.ReadLine();
            
        }

        private static void Delete(string directory)
        {
            // xmlファイルを取得し、削除する
            string[] xfiles = Directory.GetFiles(directory, "*.xml"); // 拡張子がxmlのファイル名のリストを取得（配列）

            Console.WriteLine("xmlファイルを削除しますか？　y/n");
            string deleteXml = Console.ReadLine();
            if (deleteXml == "y" || deleteXml == "Y")
            {
                foreach (string x in xfiles) // 配列のファイル名を一つずつ処理
                {
                    File.Delete(x);
                    Console.WriteLine(x);
                }
                Console.WriteLine("xmlファイルを削除しました。");
            }
            else
            {
                Console.WriteLine("xmlファイルを削除せずに続行します。");
            }



            // thmファイルを取得し、削除する
            string[] tfiles = Directory.GetFiles(directory, "*.thm"); // 拡張子がthmのファイル名のリストを取得（配列）

            Console.WriteLine("thmファイルを削除しますか？　y/n");
            string deletethm = Console.ReadLine();
            if (deletethm == "y" || deletethm == "Y")
            {
                foreach (string t in tfiles) // 配列のファイル名を一つずつ処理
                {
                    File.Delete(t);
                    Console.WriteLine(t);
                }
                Console.WriteLine("thmファイルを削除しました。");
            }
            else
            {
                Console.WriteLine("thmファイルを削除せずに続行します。");
            }

            // archive.mp4を削除する
            Console.WriteLine("archiveファイルを削除しますか？　y/n");
            string deletearc = Console.ReadLine();
            if (deletethm == "y" || deletethm == "Y")
            {
                foreach (string a in Directory.GetFiles(directory, "sm*archive*.mp4").Concat(Directory.GetFiles(directory, "so*archive*.mp4"))) // 配列のファイル名を一つずつ処理
                {
                    File.Delete(a);
                    Console.WriteLine(a);
                }
            }
            else
            {
                Console.WriteLine("archiveファイルを削除せずに続行します。");
            }

            Console.WriteLine("不要なファイルの削除が終了しました。");
        }

        static Regex regex = new Regex("\"videoTitle\":\"([^\"]+)\",\"viewCount\""); // クラスのフィールド、メンバ変数
                                                                                     /* ループの中で何度もインスタンスを生成するのは非効率なので、ループの外で1度だけ生成。*/

        /// <summary>
        /// 動画ファイルのファイル名をjsonのタイトルにリネーム
        /// </summary>
        /// <param name="jsonfilename"></param>
        static void RenameMovieFile(string jsonfilename)
        {
            string filetext = File.ReadAllText(jsonfilename, Encoding.UTF8);// jsonのすべての行を読み込む


            Match match = regex.Match(filetext); // jsonのテキストから正規表現検索しその結果がMatchに入る

            string path = Path.GetDirectoryName(jsonfilename); // ファイルパスからディレクトリを取得
            string oldFile = Path.GetFileName(jsonfilename).Replace("json", "mp4"); // jsonのファイルパスからファイル名を取得し、拡張子を置き換える。
            if (File.Exists(path + Path.DirectorySeparatorChar + oldFile))
            {
                string newMFile = match.Groups[1].Value + ".mp4"; // 正規表現検索の結果から新しいファイル名を生成
               
                StringBuilder sb = new StringBuilder(newMFile);
                sb.Replace("\\\\", "＼").Replace("\\", "").Replace('/', '／').Replace(':', '：').Replace('*', '＊').Replace('?', '？').Replace('"', '”').Replace('<', '＜').Replace('>', '＞').Replace('|', '｜');
                    
                File.Move(path + Path.DirectorySeparatorChar + oldFile, path + Path.DirectorySeparatorChar + sb.ToString()); // ファイル名とディレクトリパスからファイルパスを生成リネーム。
            }
        }
    }

    class MovieInfo
    {
        internal string Title { get; set; } // タイトル用プロパティ
        internal string Nickname { get; set; } // ニックネーム用プロパティ
        internal string Time { get; set; } // 投稿日時用プロパティ


       /* private string hoge; // フィールド

        internal string Hoge
        {
            get
            {
                return this.hoge;
            }
            private set
            {
                if (value != "だめなデータ")
                {
                    this.hoge = value + "omake";
                }
            }
        }*/
    }
}