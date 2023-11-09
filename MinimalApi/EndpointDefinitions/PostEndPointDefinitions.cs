namespace MinimalApi.EndpointDefinitions;

/*
    public class PostEndPointDefinitions : IEndPointDefinitions
    {
            private async Task<IResult> GetPostById(int id, IMediator mediator)
        {
            var getPost = new GetPostById { Id = id };
            var post = await mediator.Send(getPost);
            return TypedResults.Ok(post);
        }

        private async Task<IResult> GetAllPosts(IMediator mediator)
        {
            var getAllPost = new GetAllPosts();
            var allPost = await mediator.Send(getAllPost);
            return TypedResults.Ok(allPost);
        }

        private async Task<IResult> DeletePost(int id, IMediator mediator)
        {
            var getPost = new DeletePost { Id = id };
            await mediator.Send(getPost);
            return TypedResults.NoContent();
        }

        private async Task<IResult> UpdatePost(int id, Post post, IMediator mediator)
        {
            var getPost = new UpdatePost { Id = id, PostContent = post.Content };
            var updatePost = await mediator.Send(getPost);
            return TypedResults.Ok(updatePost);
        }

        private async Task<IResult> CreatePost(Post post, IMediator mediator)
        {
            var createPost = new CreatePost { PostContent = post.Content, FileUpload = post.FileUpload };
            var newPost = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostById", new
                { newPost.Id }, newPost);
        }

        public void RegisterEndpoint(WebApplication app)
        {
            var posts = app.MapGroup("/api/posts").AddEndpointFilter<ApiEndpointFilter>();

            posts.MapGet("/{id}", GetPostById).WithName("GetPostById");

            posts.MapGet("/", GetAllPosts);

            posts.MapPost("/", CreatePost);

            posts.MapDelete("/{id}", DeletePost);

            posts.MapPut("/{id}", UpdatePost);
        }
    } */