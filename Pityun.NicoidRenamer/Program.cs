using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pityun.NicoidRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = null; // 変数の宣言
            while (true) // 無限ループ
            {
                Console.WriteLine("フォルダのパスを入力してください。");
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

            string[] files = Directory.GetFiles(directory, "*.json"); // 拡張子がjsonのファイル名のリストを取得（配列）

            foreach (string f in files) // 配列のファイル名を一つずつ処理
            {
                RenameMovieFile(f);
                Console.WriteLine(f); // 処理対象のjsonファイルの名前を出力
            }

            Console.WriteLine("終了しました。閉じるにはEnterキーを押してください");
            Console.ReadLine();
        }

        /// <summary>
        /// 動画ファイルのファイル名をjsonのタイトルにリネーム
        /// </summary>
        /// <param name="jsonfilename"></param>
        static void RenameMovieFile(string jsonfilename) 
        {
            string filetext = File.ReadAllText(jsonfilename, Encoding.UTF8);
            string result = filetext.Replace("\",", "\",\r\n");
            File.WriteAllText(jsonfilename, result, Encoding.UTF8);
        }
    }
}