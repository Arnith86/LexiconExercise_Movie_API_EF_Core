namespace MovieCore.DomainContracts.RequestParameters;

public interface IPageList<T>
{
	IReadOnlyList<T> Items { get; }
	IPaginationMetaData MetaData { get; set; }
}