use utf8;
binmode(STDOUT, ":utf8");
binmode(STDIN, ":utf8");

while(my $t=<STDIN>){
if($t=~ /^(([^［］]+)［([^［］]+)］)(.+)$/){
print "<dt>" . basicEscape($1) . "</dt>\n" . "<key type=\"表記\">" . basicEscape($2) . "</key>\n" . "<key type=\"表記\">" . basicEscape($3) . "</key>\n" . "<dd>" . basicEscape($4) . "</dd>\n";
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
