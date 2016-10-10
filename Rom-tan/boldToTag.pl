use utf8;
binmode(STDOUT, ":utf8");
binmode(STDIN, ":utf8");

while(my $t=<STDIN>){
if($t=~ /^<bold>(.+)<\/bold>\s?(.+)$/){
print "<dt>" . basicEscape($1) . "</dt>\n";
my @keys=split(/\s/,$1);
if(@keys>1){
foreach my $item (@keys){
print "<key type=\"表記\">" . basicEscape($item) . "</key>\n";
}
}
print "<dd>" . basicEscape($2) . "</dd>\n";
}
}

sub basicEscape{
my $t=$_[0];
$t=~ s/&/&amp;/;
$t=~ s/</&lt;/;
$t=~ s/>/&gt;/;
$t=~ s/"/&quot;/;
$t=~ s/'/&apos;/;
return $t;
}