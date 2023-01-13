// <auto-generated/>
#pragma warning disable
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using SynergyISP.Domain.Entities;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertUserProfileOperation791280756
    public class UpsertUserProfileOperation791280756 : Marten.Internal.Operations.StorageOperation<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly SynergyISP.Domain.Entities.UserProfile _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertUserProfileOperation791280756(SynergyISP.Domain.Entities.UserProfile document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select synergy.mt_upsert_userprofile(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Upsert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: UpsertUserProfileOperation791280756
    
    
    // START: InsertUserProfileOperation791280756
    public class InsertUserProfileOperation791280756 : Marten.Internal.Operations.StorageOperation<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly SynergyISP.Domain.Entities.UserProfile _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertUserProfileOperation791280756(SynergyISP.Domain.Entities.UserProfile document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select synergy.mt_insert_userprofile(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Insert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: InsertUserProfileOperation791280756
    
    
    // START: UpdateUserProfileOperation791280756
    public class UpdateUserProfileOperation791280756 : Marten.Internal.Operations.StorageOperation<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly SynergyISP.Domain.Entities.UserProfile _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateUserProfileOperation791280756(SynergyISP.Domain.Entities.UserProfile document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select synergy.mt_update_userprofile(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
            postprocessUpdate(reader, exceptions);
        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            await postprocessUpdateAsync(reader, exceptions, token);
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: UpdateUserProfileOperation791280756
    
    
    // START: QueryOnlyUserProfileSelector791280756
    public class QueryOnlyUserProfileSelector791280756 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<SynergyISP.Domain.Entities.UserProfile>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyUserProfileSelector791280756(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public SynergyISP.Domain.Entities.UserProfile Resolve(System.Data.Common.DbDataReader reader)
        {

            SynergyISP.Domain.Entities.UserProfile document;
            document = _serializer.FromJson<SynergyISP.Domain.Entities.UserProfile>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<SynergyISP.Domain.Entities.UserProfile> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            SynergyISP.Domain.Entities.UserProfile document;
            document = await _serializer.FromJsonAsync<SynergyISP.Domain.Entities.UserProfile>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyUserProfileSelector791280756
    
    
    // START: LightweightUserProfileSelector791280756
    public class LightweightUserProfileSelector791280756 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<SynergyISP.Domain.Entities.UserProfile, System.Guid>, Marten.Linq.Selectors.ISelector<SynergyISP.Domain.Entities.UserProfile>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightUserProfileSelector791280756(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public SynergyISP.Domain.Entities.UserProfile Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            SynergyISP.Domain.Entities.UserProfile document;
            document = _serializer.FromJson<SynergyISP.Domain.Entities.UserProfile>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<SynergyISP.Domain.Entities.UserProfile> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            SynergyISP.Domain.Entities.UserProfile document;
            document = await _serializer.FromJsonAsync<SynergyISP.Domain.Entities.UserProfile>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightUserProfileSelector791280756
    
    
    // START: IdentityMapUserProfileSelector791280756
    public class IdentityMapUserProfileSelector791280756 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<SynergyISP.Domain.Entities.UserProfile, System.Guid>, Marten.Linq.Selectors.ISelector<SynergyISP.Domain.Entities.UserProfile>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapUserProfileSelector791280756(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public SynergyISP.Domain.Entities.UserProfile Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            SynergyISP.Domain.Entities.UserProfile document;
            document = _serializer.FromJson<SynergyISP.Domain.Entities.UserProfile>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<SynergyISP.Domain.Entities.UserProfile> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            SynergyISP.Domain.Entities.UserProfile document;
            document = await _serializer.FromJsonAsync<SynergyISP.Domain.Entities.UserProfile>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapUserProfileSelector791280756
    
    
    // START: DirtyTrackingUserProfileSelector791280756
    public class DirtyTrackingUserProfileSelector791280756 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<SynergyISP.Domain.Entities.UserProfile, System.Guid>, Marten.Linq.Selectors.ISelector<SynergyISP.Domain.Entities.UserProfile>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingUserProfileSelector791280756(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public SynergyISP.Domain.Entities.UserProfile Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            SynergyISP.Domain.Entities.UserProfile document;
            document = _serializer.FromJson<SynergyISP.Domain.Entities.UserProfile>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<SynergyISP.Domain.Entities.UserProfile> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            SynergyISP.Domain.Entities.UserProfile document;
            document = await _serializer.FromJsonAsync<SynergyISP.Domain.Entities.UserProfile>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingUserProfileSelector791280756
    
    
    // START: QueryOnlyUserProfileDocumentStorage791280756
    public class QueryOnlyUserProfileDocumentStorage791280756 : Marten.Internal.Storage.QueryOnlyDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyUserProfileDocumentStorage791280756(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(SynergyISP.Domain.Entities.UserProfile document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(SynergyISP.Domain.Entities.UserProfile document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyUserProfileSelector791280756(session, _document);
        }


        public override Npgsql.NpgsqlCommand BuildLoadCommand(System.Guid id, string tenant)
        {
            return new NpgsqlCommand(_loaderSql).With("id", id);
        }


        public override Npgsql.NpgsqlCommand BuildLoadManyCommand(System.Guid[] ids, string tenant)
        {
            return new NpgsqlCommand(_loadArraySql).With("ids", ids);
        }

    }

    // END: QueryOnlyUserProfileDocumentStorage791280756
    
    
    // START: LightweightUserProfileDocumentStorage791280756
    public class LightweightUserProfileDocumentStorage791280756 : Marten.Internal.Storage.LightweightDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightUserProfileDocumentStorage791280756(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(SynergyISP.Domain.Entities.UserProfile document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(SynergyISP.Domain.Entities.UserProfile document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightUserProfileSelector791280756(session, _document);
        }


        public override Npgsql.NpgsqlCommand BuildLoadCommand(System.Guid id, string tenant)
        {
            return new NpgsqlCommand(_loaderSql).With("id", id);
        }


        public override Npgsql.NpgsqlCommand BuildLoadManyCommand(System.Guid[] ids, string tenant)
        {
            return new NpgsqlCommand(_loadArraySql).With("ids", ids);
        }

    }

    // END: LightweightUserProfileDocumentStorage791280756
    
    
    // START: IdentityMapUserProfileDocumentStorage791280756
    public class IdentityMapUserProfileDocumentStorage791280756 : Marten.Internal.Storage.IdentityMapDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapUserProfileDocumentStorage791280756(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(SynergyISP.Domain.Entities.UserProfile document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(SynergyISP.Domain.Entities.UserProfile document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapUserProfileSelector791280756(session, _document);
        }


        public override Npgsql.NpgsqlCommand BuildLoadCommand(System.Guid id, string tenant)
        {
            return new NpgsqlCommand(_loaderSql).With("id", id);
        }


        public override Npgsql.NpgsqlCommand BuildLoadManyCommand(System.Guid[] ids, string tenant)
        {
            return new NpgsqlCommand(_loadArraySql).With("ids", ids);
        }

    }

    // END: IdentityMapUserProfileDocumentStorage791280756
    
    
    // START: DirtyTrackingUserProfileDocumentStorage791280756
    public class DirtyTrackingUserProfileDocumentStorage791280756 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingUserProfileDocumentStorage791280756(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(SynergyISP.Domain.Entities.UserProfile document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertUserProfileOperation791280756
            (
                document, Identity(document),
                session.Versions.ForType<SynergyISP.Domain.Entities.UserProfile, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(SynergyISP.Domain.Entities.UserProfile document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(SynergyISP.Domain.Entities.UserProfile document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingUserProfileSelector791280756(session, _document);
        }


        public override Npgsql.NpgsqlCommand BuildLoadCommand(System.Guid id, string tenant)
        {
            return new NpgsqlCommand(_loaderSql).With("id", id);
        }


        public override Npgsql.NpgsqlCommand BuildLoadManyCommand(System.Guid[] ids, string tenant)
        {
            return new NpgsqlCommand(_loadArraySql).With("ids", ids);
        }

    }

    // END: DirtyTrackingUserProfileDocumentStorage791280756
    
    
    // START: UserProfileBulkLoader791280756
    public class UserProfileBulkLoader791280756 : Marten.Internal.CodeGeneration.BulkLoader<SynergyISP.Domain.Entities.UserProfile, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid> _storage;

        public UserProfileBulkLoader791280756(Marten.Internal.Storage.IDocumentStorage<SynergyISP.Domain.Entities.UserProfile, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY synergy.mt_doc_userprofile(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_userprofile_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into synergy.mt_doc_userprofile (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_userprofile_temp.\"id\", mt_doc_userprofile_temp.\"data\", mt_doc_userprofile_temp.\"mt_version\", mt_doc_userprofile_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_userprofile_temp left join synergy.mt_doc_userprofile on mt_doc_userprofile_temp.id = synergy.mt_doc_userprofile.id where synergy.mt_doc_userprofile.id is null)";

        public const string OVERWRITE_SQL = "update synergy.mt_doc_userprofile target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_userprofile_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_userprofile_temp as select * from synergy.mt_doc_userprofile limit 0";


        public override string CreateTempTableForCopying()
        {
            return CREATE_TEMP_TABLE_FOR_COPYING_SQL;
        }


        public override string CopyNewDocumentsFromTempTable()
        {
            return COPY_NEW_DOCUMENTS_SQL;
        }


        public override string OverwriteDuplicatesFromTempTable()
        {
            return OVERWRITE_SQL;
        }


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, SynergyISP.Domain.Entities.UserProfile document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, SynergyISP.Domain.Entities.UserProfile document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
        {
            await writer.WriteAsync(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar, cancellation);
            await writer.WriteAsync(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb, cancellation);
        }


        public override string MainLoaderSql()
        {
            return MAIN_LOADER_SQL;
        }


        public override string TempLoaderSql()
        {
            return TEMP_LOADER_SQL;
        }

    }

    // END: UserProfileBulkLoader791280756
    
    
    // START: UserProfileProvider791280756
    public class UserProfileProvider791280756 : Marten.Internal.Storage.DocumentProvider<SynergyISP.Domain.Entities.UserProfile>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UserProfileProvider791280756(Marten.Schema.DocumentMapping mapping) : base(new UserProfileBulkLoader791280756(new QueryOnlyUserProfileDocumentStorage791280756(mapping)), new QueryOnlyUserProfileDocumentStorage791280756(mapping), new LightweightUserProfileDocumentStorage791280756(mapping), new IdentityMapUserProfileDocumentStorage791280756(mapping), new DirtyTrackingUserProfileDocumentStorage791280756(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: UserProfileProvider791280756
    
    
}

