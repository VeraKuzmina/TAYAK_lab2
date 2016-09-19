Написать программу, реализующую работу конечного автомата.

Входные данные:

1.	текстовый файл, описывающий граф переходов конечного автомата. Файл представляет собой набор строк. В каждой строке задаётся одно правило перехода в следующем виде:
     
     q<N>,<C>=<q|f><N>

     Здесь символ q обозначает состояние автомата, f – конечное состояние автомата, <N> - произвольное число, обозначающее номер состояния, <C> - один символ.

Пример: q12,g=f0 – запись означает, что если автомат находится в состоянии №12 и читает с ленты символ ‘g’, то он перейдёт в конечное состояние с номером 0

Дополнительные условия

Количество строк в файле (возможных переходов) – неограниченное количество
Начальное состояние автомата (с которого начинается его работа) – q0
Строки в файле не обязаны быть отсортированы по какому-либо критерию
Состояния автомата необязательно нумеруются последовательно

2.	строка символов, которую нужно проанализировать с помощью построенного автомата и дать заключение о возможности (или невозможности) разбора этой строки с помощью данного автомата

Пример: строки ab, abc, ba допускаются автоматом, граф переходов которого изображён на рис. 2. Строки b, ak, bad этим автоматом не допускаются.

Выходные данные:

1.	Заключение о детерминированности или недетерминированности заданного автомата.
2.	В случае недетерминированного автомата вывести переходы для соответствующего ему детерминированного автомата (в виде, соответствующем входному файлу).
3.	Заключение о возможности (невозможности) разбора автоматом введённой строки символов

Задание минимум (на 3): считать из файла автомат (файл не содержит синтаксических ошибок, заданный с его помощью автомат детерминирован), дать заключение о возможности (невозможности) разбора этим автоматом введённой строки.

Задание максимум (на 5. В принципе, совершенству нет предела, но тем не менее): считать из файла автомат (файл может содержать синтаксические ошибки, заданный с его помощью автомат недетерминирован, возможно, граф переходов содержит висячие вершины), вывести информацию об автомате (детерминирован/нет, всё, что угодно), детерминировать автомат, вывести таблицу переходов для нового автомата, разобрать входную строку и дать заключение о возможности/невозможности её разбора.

Весьма неплохо было бы при написании программы использовать возможности объектно-ориентированного программирования. В качестве дополнения, например, можно графически представить граф переходов для автомата до и после проведения операции детерминирования.

При возникновении неоднозначности в оценке, может быть предложено в качестве дополнительного задания сделать файл для автомата, разбирающего какую-то строку (например, if (a>b) exit(1);).
