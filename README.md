# RobowordConv
ロボワード形式変換用の簡単な補完ツールです。

## 概要
[Suzuto M.](http://hp.vector.co.jp/authors/VA036002/index.html)さんの[dered](http://hp.vector.co.jp/authors/VA036002/dered/)で変換されたファイルをUTF-8に変換するツールです。  
ロボワード形式での多言語対応はタグによる文字コード変更で行っているようです。
このツールは文字コード変更やその他のタグをhtml的な形式に変換します。  
このツールはROM単シリーズに付属のCD-ROMを想定して作っています。

## 手順
1. ``dered *.dic *.tmp``の要領で(バイナリ付き)テキストファイル化。
2. ``RobowordConv.exe *.tmp *.txt``の要領でUTF-8化。
3. PerlなりテキストエディタなりでEBStudio形式HTML(UTF-8)に変換。(ROM単用変換ツールは付属)
4. 必要なら複数ファイルをまとめる(仮にtotal.htmlとする)。
5. ``FontDumpK "ＭＳ ゴシック" total.html sjis.html -map=map.map``の要領でShift-JIS化。
6. EBStudioでEPWing化。

## 使い方
```RobowordConv.exe *.tmp *.txt```

deredで変換後のファイルと引数1にとり、引数2で指定されたファイルにUTF-8で書き込みます。 引数を省略すると標準入出力を利用しますが、文字コードの関係上テスト用くらいにしか使えません。

便宜的に``*.tmp``や``*.txt``としていますが、実際にはファイル名で指定してください。ワイルドカードは使えません。
複数ファイルを処理したい場合は``for``を使いましょう。覚えておくと便利です。  
例：``` for %f in (*.tmp) do RobowordConv.exe %f %~nf.txt ```(バッチファイル内では%は二つ。)

## ROM単用変換ツール
独日・仏日辞典は``Rom-tan/fgToTag.pl``で変換してください。例：``perl fgToTag.pl < fwgj.txt > fwgj.html``。  
中国語系辞典は``boldToTag.pl``で変換してください。例：``perl boldToTag.pl < rwromjsc.txt > rwromjsc.html``。

出力後ファイルにはhtmlのヘッダーなどが含まれていないので(結合を想定)、適宜追加してください。

## 対応ファイル
| 書籍名 | CD番号 | ファイル名 | ISBN | 備考 |
| -- | -- | -- | -- | -- | -- |
| 日本語>ドイツ語対照生活会話ノート | SR-00002 | rwgj.dic | 4384007779 | |
| 関口・新ドイツ語の基礎 | SR-00022 | rwgj.dic | 4384050542 | 辞書内容はSR-00002と同じ。<br />基本会話集の音声付htmlが付属。 |
| フランス語グラメール | SR-00003 | rwfj.dic | 4384008392 | |
| 日本語>フランス語対照生活会話ノート | SR-00003 | rwfj.dic | 4384007787 | |
| 日本語中国語対照生活会話ノート | SR-00004? | rwromjsc.dic<br />rwromjtc.dic<br />rwromscj.dic<br />rwromtcj.dic | 4384007795 | CD番号が誤っているかも。<br />簡体・繁体の日中・中日どちらも付属。 |

CD番号が同じなら内容も同じです。
基本的にシリーズで同じ言語は同じ内容の辞書が付属するようです。
同じタイトルでも古い版ではCD-ROMが付属しない事もありますので注意してください。

なお、SR-00001の英和辞書には対応していません。``*.dic``ファイルが見当たりません。英和辞書ならいくらでもあるので他をあたりましょう。

## 文字コード切り替えタグ
基本的にはderedのページにあるmemoを参照してください。文字コードについては併せて以下のように取り扱っています。
Microsoft拡張版を使っている事があるのでコードページとずれがある事があります。

| バイナリ | 言語 | 文字コード | コードページ | Shift-JISでの見え方 |
| -- | -- | -- |
| E1 46 30 30 | アクセント付欧文 | iso 8859-1 | 28591 | 瓰00 |
| E1 46 30 32 | ? | ? | ? | 瓰02 |
| E1 46 38 30 | 日本語 | Shift-JIS | 932 | 瓰80 |
| E1 46 38 36 | 簡体字中国語 |  gb2312 | 20936 |  瓰86 |
| E1 46 38 38 | 繁体字中国語 | big5 | 950 | 瓰88 |
